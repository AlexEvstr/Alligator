using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int _levelType;
    //private string[] _teamNames = new string[6];
    [SerializeField] private GameObject[] _gameTypes;

    [SerializeField] private Text _levelTypeText;

    private void Start()
    {
        _levelType = PlayerPrefs.GetInt("LevelType", 0);
        DisplayLevelType();
        _gameTypes[_levelType].SetActive(true);

        //_categoryType = PlayerPrefs.GetInt("CategoryType", 0);

        //LoadPlayerNames();
    }

    private void DisplayLevelType()
    {
        switch (_levelType)
        {
            case 0:
                _levelTypeText.text = "CLASSICAL";
                return;
            case 1:
                _levelTypeText.text = "TEAM";
                return;
            case 2:
                _levelTypeText.text = "FOR A WHILE";
                return;
            default :
                _levelTypeText.text = "CLASSICAL";
                break;
        }
    }

    public void PlayAgainBtn()
    {
        Invoke("PLayAgain", 0.2f);
    }

    private void PLayAgain()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void BackToMenuBtn()
    {
        Invoke("BackToMenu", 0.2f);
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}