using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelMain : MonoBehaviour, IMenu
{
    [SerializeField] private Button btnTimer;

    [SerializeField] private Button btnMoves;
    
    [SerializeField] private Button btnCycleTheme;
    [SerializeField] private Text themeText;
    
    private UIMainManager m_mngr;

    private void Awake()
    {
        if (btnMoves)btnMoves.onClick.AddListener(OnClickMoves);
        if (btnTimer)btnTimer.onClick.AddListener(OnClickTimer);
        if (btnCycleTheme)btnCycleTheme.onClick.AddListener(OnClickCycleTheme);
    }

    private void OnDestroy()
    {
        if (btnMoves) btnMoves.onClick.RemoveAllListeners();
        if (btnTimer) btnTimer.onClick.RemoveAllListeners();
        if (btnCycleTheme) btnCycleTheme.onClick.RemoveAllListeners();
    }

    public void Setup(UIMainManager mngr)
    {
        m_mngr = mngr;
        
        themeText.text = $"Theme: {m_mngr.GetCurrentThemeName()}".ToUpper();
    }

    private void OnClickTimer()
    {
        m_mngr.LoadLevelTimer();
    }

    private void OnClickMoves()
    {
        m_mngr.LoadLevelMoves();
    }

    private void OnClickCycleTheme()
    {
        m_mngr.CycleTheme();
        themeText.text = $"Theme: {m_mngr.GetCurrentThemeName()}".ToUpper();
    }
    
    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
