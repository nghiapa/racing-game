using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI bestRecord;
    public TextMeshProUGUI coinsEarned;




    public void Show(int time,int bestRecord,int money)
    {
        timeText.text = time.ToString()+"s";
        this.bestRecord.text = bestRecord.ToString()+"s";
        coinsEarned.text = money.ToString();
    }

    public void Replay()
    {
        GameManager.Instance.commandManager.SaveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        this.gameObject.SetActive(false);
    }

    public void OnClick_BackToHome()
    {
        SceneManager.LoadScene("SplashScene");
        UIManager.Instance.OnClick_RaceBtn();
        GameManager.Instance.currentGameState = EGameState.start;
        this.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        Time.timeScale = 1f; // Resume the game
    }
}
