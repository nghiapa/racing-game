using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    public GameObject selectCarPanel;
    public GameObject selectScenePanel;
    public FreeModeView freeModeView;
    public CityModeView cityModeView;
    public LosePanel losePanel;
    public SettingPanel settingPanel;

    public List<GameObject> viewList;

    private void Start()
    {
        OnClick_SelectCarBtn();
    }

    public void CloseAllView()
    {
        foreach (GameObject view in viewList)
        {
            view.SetActive(false);
        }
    }

    public void OnClick_RaceBtn()
    {
        CloseAllView();
        selectScenePanel.SetActive(true);
    }


    public void OnClick_SelectCarBtn()
    {
        CloseAllView();
        selectCarPanel.SetActive(true);
    }

    public void OnClick_FreeModeBtn()
    {
        CloseAllView();
        freeModeView.gameObject.SetActive(true);
    }

    public void OnClick_CityModeBtn()
    {
        CloseAllView();
        cityModeView.gameObject.SetActive(true);
    }

    public void OnClick_Setting() 
    {
        CloseAllView();
        settingPanel.gameObject.SetActive(true);
    }

    public void ShowLosePanel()
    {
        losePanel.gameObject.SetActive(true);
        PlayerProfile playerProfile = GameManager.Instance.playerProfile;
        losePanel.Show(playerProfile.bestTime,playerProfile.time,playerProfile.moneyEarned);
    }

    
}
