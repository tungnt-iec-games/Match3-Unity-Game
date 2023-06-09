using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action<eStateGame> StateChangedAction = delegate { };

    [SerializeField] private GameSettings m_gameSettings;
    [SerializeField] private ItemData itemData;
    [SerializeField] private BoardController m_boardController;
    [SerializeField] private UIMainManager m_uiMenu;
    [SerializeField] private FxHub fxHub;
    [SerializeField] private ItemCounter itemCounter;
    [SerializeField] private eStateGame m_state;
    [SerializeField] private eLevelMode mode;
    private LevelCondition m_levelCondition;

    private bool onRestart;

    public static GameManager Instance;
    public ItemData ItemData => itemData;
    public FxHub FxHub => fxHub;
    public ItemCounter ItemCounter => itemCounter;

    public enum eLevelMode
    {
        TIMER,
        MOVES
    }

    public enum eStateGame
    {
        SETUP,
        MAIN_MENU,
        GAME_STARTED,
        PAUSE,
        GAME_OVER,
    }

    public eStateGame State
    {
        get { return m_state; }
        private set
        {
            m_state = value;

            StateChangedAction(m_state);
        }
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        State = eStateGame.SETUP;

        if (m_gameSettings == null) //optimal performance
            m_gameSettings = Resources.Load<GameSettings>(Constants.GAME_SETTINGS_PATH);

        if (m_uiMenu == null) //optimal performance
            m_uiMenu = FindObjectOfType<UIMainManager>();
        m_uiMenu.Setup(this);
    }

    void Start()
    {
        State = eStateGame.MAIN_MENU;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_boardController != null) m_boardController.Update();
    }


    internal void SetState(eStateGame state)
    {
        State = state;
        if (State == eStateGame.PAUSE)
        {
            DOTween.PauseAll();
        }
        else
        {
            DOTween.PlayAll();
        }
    }

    public void LoadLevel(eLevelMode mode)
    {
        if (m_boardController == null) //optimal performance
            m_boardController = new GameObject("BoardController").AddComponent<BoardController>();
        m_boardController.StartGame(this, m_gameSettings);
        this.mode = mode;
        if (mode == eLevelMode.MOVES)
        {
            m_levelCondition = this.gameObject.AddComponent<LevelMoves>();
            m_levelCondition.Setup(m_gameSettings.LevelMoves, m_uiMenu.GetLevelConditionView(), m_boardController);
        }
        else if (mode == eLevelMode.TIMER)
        {
            m_levelCondition = this.gameObject.AddComponent<LevelTime>();
            m_levelCondition.Setup(m_gameSettings.LevelMoves, m_uiMenu.GetLevelConditionView(), this);
        }

        m_levelCondition.ConditionCompleteEvent += GameOver;

        State = eStateGame.GAME_STARTED;
    }

    public void GameOver()
    {
        StartCoroutine(WaitBoardController());
    }

    internal void ClearLevel()
    {
        if (m_boardController)
        {
            m_boardController.Clear();
            Destroy(m_boardController.gameObject);
            m_boardController = null;
        }
    }

    private IEnumerator WaitBoardController()
    {
        while (m_boardController.IsBusy)
        {
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1f);

        State = eStateGame.GAME_OVER;

        if (m_levelCondition != null)
        {
            m_levelCondition.ConditionCompleteEvent -= GameOver;

            Destroy(m_levelCondition);
            m_levelCondition = null;
        }
    }

    public void SetOnRestart(bool status)
    {
        onRestart = status;
    }

    public void OnRestart()
    {
        if (onRestart)
        {
            itemCounter.ResetAll();
            SetOnRestart(false);
            LoadLevel(mode);
        }
    }
}
