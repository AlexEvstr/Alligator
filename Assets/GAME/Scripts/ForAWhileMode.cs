using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ForAWhileMode : MonoBehaviour
{
    public WordManager wordManager;
    public Text wordText, scoreText, titleText, timerText, finishScoreText, finishScoreText2;
    public Button guessedRightButton, skipButton;
    public GameObject FinishObject, AgainBtn, MenuBtn, finish2;

    private List<string> _words;
    private int _currentWordIndex = 0;
    private int _score = 0;
    private float _timer = 120f; // 2 минуты

    [SerializeField] private GameObject _pausePopup;

    private void Start()
    {
        int _categoryType = PlayerPrefs.GetInt("CategoryType", 0);
        LoadWords(_categoryType);
        titleText.text = (_categoryType == 8) ? "Random Words" : wordManager.categories[_categoryType].categoryName;
        StartCoroutine(TimerCoroutine());
        ShowNextWord();
    }

    private void LoadWords(int categoryType)
    {
        if (categoryType == 8)
        {
            _words = wordManager.categories.SelectMany(c => c.words).OrderBy(x => Random.value).ToList();
        }
        else
        {
            _words = wordManager.categories[categoryType].words.OrderBy(x => Random.value).ToList();
        }
    }

    private void ShowNextWord()
    {
        if (_currentWordIndex < 20)
        {
            wordText.text = _words[_currentWordIndex];
            _currentWordIndex++;
        }
        else
        {
            Debug.Log("GameType_3_GameOver");
            GameOver();
            finish2.SetActive(true);
        }
    }

    public void OnGuessedRight()
    {
        _score++;
        scoreText.text = _score.ToString();
        finishScoreText.text = _score.ToString();
        finishScoreText2.text = _score.ToString();
        ShowNextWord();
    }

    public void OnSkip()
    {
        ShowNextWord();
    }

    private IEnumerator TimerCoroutine()
    {
        while (_timer > 0)
        {
            if (!_pausePopup.activeInHierarchy)
            {
                timerText.text = $"{(int)_timer / 60}:{(int)_timer % 60:D2}";
                yield return new WaitForSeconds(1);
                _timer--;
            }
            yield return null;
        }
        GameOver();
        FinishObject.SetActive(true);
    }

    private void GameOver()
    {
        wordText.text = "";
        Debug.Log("GameType_3_GameOver");
        timerText.gameObject.SetActive(false);
        guessedRightButton.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(false);
        
        AgainBtn.SetActive(true);
        MenuBtn.SetActive(true);
    }
}