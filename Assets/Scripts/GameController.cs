using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void BackToMenuBtn()
    {
        Invoke("MenuBtn", 0.25f);

    }

    private void MenuBtn()
    {
        SceneManager.LoadScene("Scene_Menu");
    }
}