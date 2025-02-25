using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChooser : MonoBehaviour
{
    private void Start()
    {
        int bestLevel = PlayerPrefs.GetInt("BestLevel", 1);
        if (bestLevel >= int.Parse(gameObject.name))
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            GetComponent<Button>().interactable = false;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void ChooseLevel(int levelIndex)
    {
        PlayerPrefs.SetInt("LevelCurrent", levelIndex);
        StartCoroutine(StartGameWithDelay());
    }

    private IEnumerator StartGameWithDelay()
    {
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene("Scene_Game");
    }
}