using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    public Sprite musicOn;
    public Sprite musicOff;
    public Image musicIcon;



    private void OnEnable()
    {
        bool isMusicOn = PlayerPrefs.GetInt("Music", 1) == 1;

        musicIcon.sprite = isMusicOn ? musicOn : musicOff;

    }

    public void OnClick_Music()
    {
        bool isMusicOn = PlayerPrefs.GetInt("Music", 1) == 1;
        if (isMusicOn)
        {
            // Turn off music
            AudioListener.volume = 0f;
            PlayerPrefs.SetInt("Music", 0);
            musicIcon.sprite = musicOff;
        }
        else
        {
            // Turn on music
            AudioListener.volume = 1f;
            PlayerPrefs.SetInt("Music", 1);
            musicIcon.sprite = musicOn;
        }
    }

    public void OnClick_Close()
    {
        Time.timeScale = 1f; // Resume the game
        this.gameObject.SetActive(false);
        GameManager.Instance.currentGameState = EGameState.playing;
    }
}
