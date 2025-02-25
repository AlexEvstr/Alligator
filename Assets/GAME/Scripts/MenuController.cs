using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void ExitGameButton()
    {
        Invoke("ExitGame", 0.35f);
        
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    public void StartGameButton()
    {
        Invoke("StartGame", 0.25f);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
}