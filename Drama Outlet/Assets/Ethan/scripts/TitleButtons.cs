using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtons : MonoBehaviour
{
    public int SceneToLoad;
    public void StartGame()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
