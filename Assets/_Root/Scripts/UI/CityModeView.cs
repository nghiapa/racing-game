using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CityModeView : MonoBehaviour
{
    public GameObject pausePanel;
    public Joystick joystick;

    public TextMeshProUGUI money; 

    private void Start()
    {
        EventManager.Event_OnPlayerCointChange += (amt) =>
        {
            money.text = amt.ToString();
        };
        GameManager.Instance.currentGameState = EGameState.playing;

    }

    private void OnEnable()
    {
        pausePanel.SetActive(false); 

        GameManager.Instance.joystick = joystick; 
    }

    public void OnClick_Setting()
    {
        Time.timeScale = 0f; // Pause the game
        pausePanel.SetActive(true);
        GameManager.Instance.currentGameState = EGameState.Pause;
    }

    public void OnClick_CloseSetting()
    {
        Time.timeScale = 1f; // Resume the game
        pausePanel.SetActive(false);
        GameManager.Instance.currentGameState = EGameState.playing;
    }

    public void OnClick_BackToHome()
    {
        SceneManager.LoadScene("SplashScene");
        UIManager.Instance.OnClick_RaceBtn();
        GameManager.Instance.currentGameState = EGameState.start;
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
