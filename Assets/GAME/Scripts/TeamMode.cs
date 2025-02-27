using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TeamMode : MonoBehaviour
{
    public WordManager wordManager;
    public Text wordText, scoreText, titleText, timerText, teamScoreText, teamNameText, skipText, finishScoreText;
    public Button guessedRightButton, skipButton, nextTeamButton, resultsButton;
    public GameObject timeUpText, nextTeamPanel, teamResultsPanel;
    public GameObject[] teamResultCells;
    public GameObject CoinWithScore;
    public GameObject _pausePopup;

    private List<string> _words;
    private int _currentWordIndex = 0;
    private int _score = 0;
    private float _timer = 6f; // 1 минута
    private int _skipCount = 0;
    private string[] _teamNames = new string[6];
    private int[] _teamScores = new int[6];
    private int _currentTeamIndex = 0;
    private int _roundsPlayed = 0;
    private const int _maxRounds = 3;
    public OptionsController optionsController;

    private void Start()
    {
        int _categoryType = PlayerPrefs.GetInt("CategoryType", 0);
        LoadWords(_categoryType);
        LoadPlayerNames();
        ShowNextTeam();
    }

    private void LoadWords(int categoryType)
    {
        if (categoryType == 8) // Режим Random Words
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
            optionsController.DisableBGMusic();
            optionsController.PlayWinSound();
            return;
        }

        if (_currentTeamIndex >= _teamNames.Length || string.IsNullOrEmpty(_teamNames[_currentTeamIndex]))
        {
            _currentTeamIndex = 0;
            _roundsPlayed++;
            ShowNextTeam();
            return;
        }

        // Обновляем UI
        teamNameText.text = _teamNames[_currentTeamIndex];
        _score = 0;
        scoreText.text = "0";
        finishScoreText.text = _teamScores[_currentTeamIndex].ToString(); // Показываем очки с прошлого раунда
        _skipCount = 0;
        skipText.text = "Skip 0/3";
        timeUpText.SetActive(false);
        nextTeamPanel.SetActive(false);
        skipButton.interactable = true;
        guessedRightButton.interactable = true;
        wordText.text = "";

        ShowNextWord();
        StartCoroutine(TimerCoroutine());
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
            _currentWordIndex = 0; // Слова закончились → перемешиваем и заново
            _words = _words.OrderBy(x => Random.value).ToList();
            ShowNextWord();
        }
    }

    public void OnGuessedRight()
    {
        _teamScores[_currentTeamIndex] += 1;
        finishScoreText.text = _teamScores[_currentTeamIndex].ToString();
        _score++;
        scoreText.text = _score.ToString();
        
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
            if (!_pausePopup.activeInHierarchy)
            {
                timerText.text = $"{(int)_timer / 60}:{(int)_timer % 60:D2}";
                if (_timer == 1 || _timer == 2 || _timer == 3) optionsController.PlayChickSound();
                
                yield return new WaitForSeconds(1);
                _timer--;
                if (_timer == 0) optionsController.PlayEndTimeSound();
            }
            yield return null;
        }
        EndTeamTurn();
    }

    private void EndTeamTurn()
    {
        // Сохраняем очки команды
        

        // Выключаем ненужные элементы
        timerText.gameObject.SetActive(false);
        guessedRightButton.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(false);
        timeUpText.SetActive(true);
        nextTeamPanel.SetActive(true);
        CoinWithScore.SetActive(true);
        wordText.text = "";

        if (_roundsPlayed+1 >= _maxRounds)
        {
            if (_currentTeamIndex+1 >= _teamNames.Length || (string.IsNullOrEmpty(_teamNames[_currentTeamIndex + 1])))
            {
                nextTeamButton.gameObject.SetActive(false);
                resultsButton.gameObject.SetActive(true);
            } 
        }
    }

    public void OnNextTeam()
    {
        _timer = 6f; // Сбрасываем таймер
        CoinWithScore.SetActive(false);
        timerText.gameObject.SetActive(true);
        guessedRightButton.gameObject.SetActive(true);
        skipButton.gameObject.SetActive(true);
        timeUpText.SetActive(false);
        nextTeamPanel.SetActive(false);
        _currentTeamIndex++;
        ShowNextTeam();
    }

    private void ShowFinalResults()
    {
        teamResultsPanel.SetActive(true);

        var sortedTeams = _teamScores
            .Select((score, index) => new { Score = score, Name = _teamNames[index] })
            .OrderByDescending(t => t.Score)
            .Where(t => !string.IsNullOrEmpty(t.Name))
            .ToList();

        // Выключаем лишние ячейки
        int totalTeams = sortedTeams.Count;
        for (int i = 0; i < teamResultCells.Length; i++)
        {
            if (i >= totalTeams)
            {
                teamResultCells[i].SetActive(false);
            }
            else
            {
                teamResultCells[i].SetActive(true);
                teamResultCells[i].transform.GetChild(0).GetComponent<Text>().text = sortedTeams[i].Name; // Название команды
                teamResultCells[i].transform.GetChild(1).GetComponent<Text>().text = sortedTeams[i].Score.ToString(); // Очки
            }
        }
    }
}
