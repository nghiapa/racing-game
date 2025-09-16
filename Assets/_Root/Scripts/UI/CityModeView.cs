using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CityModeView : MonoBehaviour
{
    public Joystick joystick;

    public TextMeshProUGUI money; 
    PlayerProfile playerProfile;



    private void Awake()
    {
        playerProfile = GameManager.Instance.playerProfile;
        money.text = playerProfile.GetResourceAmount(EgameResource.money).ToString();
    }
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

        GameManager.Instance.joystick = joystick; 
        GameManager.Instance.currentGameMode = EGameMode.cityMode;
    }

    public void OnClick_Setting()
    {
        Time.timeScale = 0f; // Pause the game
        UIManager.Instance.OnClick_Setting();
        GameManager.Instance.currentGameState = EGameState.Pause;
    }

    public void OnClick_BackToHome()
    {
        SceneManager.LoadScene("SplashScene");
        UIManager.Instance.OnClick_RaceBtn();
        GameManager.Instance.currentGameState = EGameState.start;
        this.gameObject.SetActive(false);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        this.gameObject.SetActive(false);
    }
}
