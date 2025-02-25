using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WindowSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject windowToClose;
    [SerializeField] private GameObject windowToOpen;

    private CanvasGroup closeCanvasGroup;
    private CanvasGroup openCanvasGroup;

    private void Awake()
    {
        if (windowToClose != null)
        {
            closeCanvasGroup = windowToClose.GetComponent<CanvasGroup>();
            if (closeCanvasGroup == null)
                closeCanvasGroup = windowToClose.AddComponent<CanvasGroup>();
        }

        if (windowToOpen != null)
        {
            openCanvasGroup = windowToOpen.GetComponent<CanvasGroup>();
            if (openCanvasGroup == null)
                openCanvasGroup = windowToOpen.AddComponent<CanvasGroup>();

            //windowToOpen.SetActive(true);
            openCanvasGroup.alpha = 0f;
            openCanvasGroup.interactable = false;
            openCanvasGroup.blocksRaycasts = false;
        }
    }

    public void SwitchWindows()
    {
        StartCoroutine(FadeOutAndIn());
    }

    private IEnumerator FadeOutAndIn()
    {
        if (windowToClose != null)
        {
            for (float t = 1f; t >= 0f; t -= Time.deltaTime * 5)
            {
                closeCanvasGroup.alpha = t;
                yield return null;
            }
            closeCanvasGroup.alpha = 0f;
        }

        if (windowToOpen != null)
        {
            windowToOpen.SetActive(true);
            openCanvasGroup.interactable = true;
            openCanvasGroup.blocksRaycasts = true;

            ScrollRect scrollView = windowToOpen.GetComponentInChildren<ScrollRect>();

            if (scrollView != null)
            {
                yield return null;
                scrollView.verticalNormalizedPosition = 1f;
            }

            for (float t = 0f; t <= 1f; t += Time.deltaTime * 5)
            {
                openCanvasGroup.alpha = t;
                yield return null;
            }
            openCanvasGroup.alpha = 1f;
        }
        if (windowToClose != null) windowToClose.SetActive(false);
    }
}