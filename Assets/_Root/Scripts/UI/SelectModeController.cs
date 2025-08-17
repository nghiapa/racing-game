using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectModeController : MonoBehaviour
{

    public void OnClick_CityMode()
    {
        UIManager.Instance.OnClick_CityModeBtn();
        SceneManager.LoadScene("WorldScene");
    }

    public void OnClick_FreeMode()
    {
        UIManager.Instance.OnClick_FreeModeBtn();
        SceneManager.LoadScene("PracticeScene");
    }
}
