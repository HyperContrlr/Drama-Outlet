using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtons : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
    public void CreditsLoad()
    {
        SceneManager.LoadScene(2);
    }
}
