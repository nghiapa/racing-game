using UnityEngine;
using UnityEngine.SceneManagement;

public class FreeModeView : MonoBehaviour
{
    public GameObject pausePanel;



    private void OnEnable()
    {
        pausePanel.SetActive(false); // Ensure the pause panel is hidden at the start

    }

    public void OnClick_Setting()
    {
        Time.timeScale = 0f; // Pause the game
        pausePanel.SetActive(true);
    }

    public void OnClick_CloseSetting()
    {
        Time.timeScale = 1f; // Resume the game
        pausePanel.SetActive(false);
    }

    public void OnClick_BackToHome()
    {
        SceneManager.LoadScene("SplashScene");
        UIManager.Instance.OnClick_RaceBtn();
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
