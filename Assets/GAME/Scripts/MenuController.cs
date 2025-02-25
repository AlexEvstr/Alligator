using UnityEngine;

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
}