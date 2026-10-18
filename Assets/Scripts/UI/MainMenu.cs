using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator playAnim;
    [SerializeField] private Animator quitAnim;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
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

}
