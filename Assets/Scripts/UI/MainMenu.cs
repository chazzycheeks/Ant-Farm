using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator playAnim;
    [SerializeField] private Animator quitAnim;
    [SerializeField] private Animator settingsAnim;
    [SerializeField] private Animator creditsAnim;
    [SerializeField] private Animator backAnim;
    [SerializeField] private GameObject creditsScreen;
    [SerializeField] private GameObject settingsScreen;

    [SerializeField] private StudioEventEmitter menuEmitter;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        menuEmitter.Stop();
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        settingsScreen.SetActive(true);
    }
    public void OpenCredits()
    {
        creditsScreen.SetActive(true);
    }
    public void BackButton()
    {
        settingsScreen.SetActive(false);
        creditsScreen.SetActive(false);
    }

    public void ChangePlayColourGreen()
    {
        playAnim.SetBool("hovering", true);
    }
    public void ChangePlayColourWhite()
    {
        playAnim.SetBool("hovering", false);
    }
    public void ChangeQuitColourGreen()
    {
        quitAnim.SetBool("hovering", true);
    }
    public void ChangeQuitColourWhite()
    {
        quitAnim.SetBool("hovering", false);
    }
    public void ChangeCreditsColourGreen()
    {
        quitAnim.SetBool("hovering", true);
    }
    public void ChangeCreditsColourWhite()
    {
        quitAnim.SetBool("hovering", false);
    }
    public void ChangeSettingsColourGreen()
    {
        quitAnim.SetBool("hovering", true);
    }
    public void ChangeSettingsColourWhite()
    {
        quitAnim.SetBool("hovering", false);
    }
    public void ChangeBackColourGreen()
    {
        quitAnim.SetBool("hovering", true);
    }
    public void ChangeBackColourWhite()
    {
        quitAnim.SetBool("hovering", false);
    }

}
