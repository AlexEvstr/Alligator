using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TeamMode : MonoBehaviour
{
    public WordManager wordManager;
    public Text wordText, scoreText, titleText, timerText, teamScoreText, teamNameText, skipText;
    public Button guessedRightButton, skipButton, nextTeamButton;
    public GameObject timeUpText, nextTeamPanel, teamResultsPanel;
    public Transform teamResultsContainer;
    public GameObject teamResultPrefab;

    private List<string> _words;
    private int _currentWordIndex = 0;
    private int _score = 0;
    private float _timer = 60f;
    private int _skipCount = 0;
    private string[] _teamNames = new string[6];
    private int[] _teamScores = new int[6];
    private int _currentTeamIndex = 0;
    private int _roundsPlayed = 0;
    private const int _maxRounds = 3;

    private void Start()
    {
        int _categoryType = PlayerPrefs.GetInt("CategoryType", 0);
        LoadWords(_categoryType);
        LoadPlayerNames();
        ShowNextTeam();
    }

    private void LoadWords(int categoryType)
    {
        if (categoryType == 9)
        {
            _words = wordManager.categories.SelectMany(c => c.words).OrderBy(x => Random.value).ToList();
        }
        else
        {
            _words = wordManager.categories[categoryType].words.OrderBy(x => Random.value).ToList();
        }
    }

    private void LoadPlayerNames()
    {
        for (int i = 0; i < 6; i++)
        {
            if (PlayerPrefs.HasKey("PlayerName" + i))
            {
                string name = PlayerPrefs.GetString("PlayerName" + i);
                if (!name.Contains("name ") && name != "")
                {
                    _teamNames[i] = name;
                }
            }
        }
    }

    private void ShowNextTeam()
    {
        if (_roundsPlayed >= _maxRounds)
        {
            ShowFinalResults();
            return;
        }

        if (_currentTeamIndex >= _teamNames.Length || string.IsNullOrEmpty(_teamNames[_currentTeamIndex]))
        {
            _currentTeamIndex = 0;
            _roundsPlayed++;
            ShowNextTeam();
            return;
        }

        teamNameText.text = _teamNames[_currentTeamIndex];
        _score = 0;
        scoreText.text = "Score: 0";
        _skipCount = 0;
        skipText.text = "Skip 0/3";
        timeUpText.SetActive(false);
        nextTeamPanel.SetActive(false);
        skipButton.interactable = true;
        StartCoroutine(TimerCoroutine());
    }

    public void OnGuessedRight()
    {
        _score++;
        scoreText.text = "Score: " + _score;
        ShowNextWord();
    }

    public void OnSkip()
    {
        if (_skipCount < 3)
        {
            _skipCount++;
            skipText.text = $"Skip {_skipCount}/3";
            if (_skipCount == 3) skipButton.interactable = false;
            ShowNextWord();
        }
    }

    private IEnumerator TimerCoroutine()
    {
        while (_timer > 0)
        {
            timerText.text = $"{(int)_timer / 60}:{(int)_timer % 60:D2}";
            yield return new WaitForSeconds(1);
            _timer--;
        }
        timeUpText.SetActive(true);
        _teamScores[_currentTeamIndex] += _score;
        _currentTeamIndex++;
        ShowNextTeam();
    }

    private void ShowFinalResults()
    {
        Debug.Log("Game Over");
    }

    private void ShowNextWord()
    {
        if (_currentWordIndex < _words.Count)
        {
            wordText.text = _words[_currentWordIndex];
            _currentWordIndex++;
        }
        else
        {
            _currentWordIndex = 0; // Перемешиваем и показываем заново
            _words = _words.OrderBy(x => Random.value).ToList();
            ShowNextWord();
        }
    }
}
