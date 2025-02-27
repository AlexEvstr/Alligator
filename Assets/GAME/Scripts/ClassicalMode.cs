using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class ClassicalMode : MonoBehaviour
{
    public WordManager wordManager;
    public Text wordText, scoreText, titleText, finishScoreText;
    public GameObject FinishObject, AgainBtn, MenuBtn;
    public Button guessedRightButton, skipButton;

    private List<string> _words;
    private int _currentWordIndex = 0;
    private int _score = 0;

    private void Start()
    {
        int _categoryType = PlayerPrefs.GetInt("CategoryType", 0);
        LoadWords(_categoryType);
        titleText.text = (_categoryType == 8) ? "Random Words" : wordManager.categories[_categoryType].categoryName;
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
            wordText.text = "";
            FinishObject.SetActive(true);
            FinishObject.SetActive(true);
            AgainBtn.SetActive(true);
            MenuBtn.SetActive(true);
            guessedRightButton.gameObject.SetActive(false);
            skipButton.gameObject.SetActive(false);
        }
    }

    public void OnGuessedRight()
    {
        _score++;
        scoreText.text = _score.ToString();
        finishScoreText.text = _score.ToString();
        ShowNextWord();
    }

    public void OnSkip()
    {
        ShowNextWord();
    }
}
