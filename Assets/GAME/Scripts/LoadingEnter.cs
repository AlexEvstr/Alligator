using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingEnter : MonoBehaviour
{
    public Image fillBar;
    private float fillTime = 2.2f;

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        StartCoroutine(FillBarAndLoadScene());
    }

    IEnumerator FillBarAndLoadScene()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fillTime)
        {
            elapsedTime += Time.deltaTime;
            fillBar.fillAmount = Mathf.Clamp01(elapsedTime / fillTime);
            yield return null;
        }
        SceneManager.LoadScene("MainMenu");
    }
}
