using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action<eStateGame> StateChangedAction = delegate { };

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

    private eStateGame m_state;
    public eStateGame State
    {
        get { return m_state; }
        private set
        {
            m_state = value;

            StateChangedAction(m_state);
        }
    }
    
    [SerializeField] private GameSettings GameSettings;

    private BoardController m_boardController;

    [SerializeField] private UIMainManager UIMenu;

    private LevelCondition m_levelCondition;

    private eLevelMode m_currentMode;

    private void Awake()
    {
        State = eStateGame.SETUP;

        GameSettings.Init();

        if (!UIMenu)
        {
            UIMenu = GameObject.FindWithTag("UI").GetComponent<UIMainManager>();
        }

        UIMenu.Setup(this);
    }

    void Start()
    {
        State = eStateGame.MAIN_MENU;
    }
    
    internal void SetState(eStateGame state)
    {
        State = state;

        if(State == eStateGame.PAUSE)
        {
            Time.timeScale = 0f;
            DOTween.PauseAll();
        }
        else
        {
            Time.timeScale = 1f;
            DOTween.PlayAll();
        }
    }

    public void LoadLevel(eLevelMode mode)
    {
        if (m_boardController)
        {
            m_boardController.gameObject.SetActive(true);
        }
        else
        {
            m_boardController = new GameObject("BoardController").AddComponent<BoardController>();
            m_boardController.Init(this, GameSettings);
        }
        
        m_boardController.StartGame();
        
        m_currentMode = mode;
        
        SetupLevelCondition(m_currentMode);
    }

    public void RestartLevel()
    {
        m_boardController.ResetBoard();
        
        if (m_levelCondition != null)
        {
            m_levelCondition.ConditionCompleteEvent -= GameOver;

            Destroy(m_levelCondition);
            m_levelCondition = null;
        }
        
        SetupLevelCondition(m_currentMode);
    }

    private void SetupLevelCondition(eLevelMode mode)
    {
        if (mode == eLevelMode.MOVES)
        {
            m_levelCondition = this.gameObject.AddComponent<LevelMoves>();
            m_levelCondition.Setup(GameSettings.LevelMoves, UIMenu.GetLevelConditionView(), m_boardController);
        }
        else if (mode == eLevelMode.TIMER)
        {
            m_levelCondition = this.gameObject.AddComponent<LevelTime>();
            m_levelCondition.Setup(GameSettings.LevelMoves, UIMenu.GetLevelConditionView(), this);
        }

        m_levelCondition.ConditionCompleteEvent += GameOver;

        State = eStateGame.GAME_STARTED;
    }

    public void CycleTheme()
    {
        GameSettings.CycleTheme();
    }

    public string GetCurrentThemeName()
    {
        return GameSettings.CurrentNormalItemConfig.ThemeName;
    }
    
    public void GameOver()
    {
        StartCoroutine(WaitBoardController());
    }

    internal void ClearLevel()
    {
        if (m_boardController)
        {
            m_boardController.RemoveBoard();
            PrefabDictionaryPool.Clear();
            m_boardController.gameObject.SetActive(false);
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
}
