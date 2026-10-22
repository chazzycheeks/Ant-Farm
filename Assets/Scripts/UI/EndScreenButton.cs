using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenButton : MonoBehaviour
{
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
        AudioManager.instance.StopEndingMusic();
        AudioManager.instance.StopAmbience();

    }
    public void PauseMainMenuButton()
    {
        SceneManager.LoadScene(0);
        AudioManager.instance.StopAllMusic();
    }
}
