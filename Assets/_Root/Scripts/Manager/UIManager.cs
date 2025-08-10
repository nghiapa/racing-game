using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{


    public void SelectSceneWorld()
    {
        SceneManager.LoadScene("WorldScene");
    }

    public void SelectPracticeScene()
    {
        SceneManager.LoadScene("PracticeScene");
    }
}
