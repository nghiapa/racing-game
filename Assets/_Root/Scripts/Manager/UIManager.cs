using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    public GameObject selectCarPanel;
    public GameObject selectScenePanel;
    public GameObject freeModeView;
    public GameObject cityModeView;

    public List<GameObject> viewList;


    private void Start()
    {
        OnClick_SelectCarBtn();
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
        freeModeView.SetActive(true);
    }

    public void OnClick_CityModeBtn()
    {
        CloseAllView();
        cityModeView.SetActive(true);
    }

    public void CloseAllView()
    {
        foreach (GameObject view in viewList)
        {
            view.SetActive(false);
        }
    }
    public void OnClick_Setting() 
    {

    }
}
