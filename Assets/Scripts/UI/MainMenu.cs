using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator playAnim;
    [SerializeField] private Animator quitAnim;
    [SerializeField] private Animator creditsAnim;
    [SerializeField] private Animator backAnim;

    [SerializeField] private GameObject play;
    [SerializeField] private GameObject quit;
    [SerializeField] private GameObject credits;

    [SerializeField] private GameObject creditsScreen;
/*    [SerializeField] private Slider volume;*/

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

    public void OpenCredits()
    {
        creditsScreen.SetActive(true);
        play.SetActive(false);
        quit.SetActive(false);
        credits.SetActive(false);
    }
    public void BackButton()
    {
        creditsScreen.SetActive(false);
        play.SetActive(true);
        quit.SetActive(true);
        credits.SetActive(true);
    }

 /*   public void ChangeVolume()
    {
        *//*volume.*//*
    }*/

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
        creditsAnim.SetBool("hovering", true);
    }
    public void ChangeCreditsColourWhite()
    {
        creditsAnim.SetBool("hovering", false);
    }
    public void ChangeBackColourGreen()
    {
        backAnim.SetBool("hovering", true);
    }
    public void ChangeBackColourWhite()
    {
        backAnim.SetBool("hovering", false);
    }

}
