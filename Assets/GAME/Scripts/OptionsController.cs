using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] private GameObject _onSound;
    [SerializeField] private GameObject _offSound;
    [SerializeField] private GameObject _onMusic;
    [SerializeField] private GameObject _offMusic;
    [SerializeField] private GameObject _onVibro;
    [SerializeField] private GameObject _offVibro;

    [SerializeField] private AudioSource _soundController;
    [SerializeField] private AudioSource _musicController;

    [SerializeField] private AudioClip _clickSound;

    [SerializeField] private GameObject _backBtn;
    [SerializeField] private GameObject _saveBtn;

    [SerializeField] private AudioClip _tic;
    [SerializeField] private AudioClip _endTime;
    [SerializeField] private AudioClip _win;
    [SerializeField] private AudioClip _correct;
    [SerializeField] private AudioClip _wrong;

    [SerializeField] private GameObject _lightTheme;
    [SerializeField] private GameObject _darkTheme;
    [SerializeField] private Sprite[] _themesSprites;
    [SerializeField] private Image[] _themesImages;

    private int _themeIndex;

    private float _vibration;

    private void Start()
    {
        Vibration.Init();

        _soundController.volume = PlayerPrefs.GetFloat("SoundVolume", 1);
        if (_soundController.volume == 1)
        {
            _onSound.SetActive(true);
            _offSound.SetActive(false);
        }
        else
        {
            _offSound.SetActive(true);
            _onSound.SetActive(false);
        }

        _musicController.volume = PlayerPrefs.GetFloat("MusicVolume", 1);
        if (_musicController.volume == 1)
        {
            _onMusic.SetActive(true);
            _offMusic.SetActive(false);
        }
        else
        {
            _offMusic.SetActive(true);
            _onMusic.SetActive(false);
        }

        _vibration = PlayerPrefs.GetFloat("VibroStatus", 1);

        if (_vibration == 1)
        {
            _onVibro.SetActive(true);
            _offVibro.SetActive(false);
        }
        else
        {
            _offVibro.SetActive(true);
            _onVibro.SetActive(false);
        }

        _themeIndex = PlayerPrefs.GetInt("ThemeIndex", 0);
        if (_themeIndex == 0) SwithThemeToLight();
        else SwithThemeToDark();

        
    }

    public void DisableSound()
    {
        _offSound.SetActive(true);
        _onSound.SetActive(false);
        _soundController.volume = 0;
        PlayerPrefs.SetFloat("SoundVolume", 0);
        ShowSaveButton();
    }

    public void EnableSound()
    {
        _onSound.SetActive(true);
        _offSound.SetActive(false);
        _soundController.volume = 1;
        PlayerPrefs.SetFloat("SoundVolume", 1);
        ShowSaveButton();
    }

    public void DisableMusic()
    {
        _offMusic.SetActive(true);
        _onMusic.SetActive(false);
        _musicController.volume = 0;
        PlayerPrefs.SetFloat("MusicVolume", 0);
        ShowSaveButton();
    }

    public void EnableMusic()
    {
        _onMusic.SetActive(true);
        _offMusic.SetActive(false);
        _musicController.volume = 1;
        PlayerPrefs.SetFloat("MusicVolume", 1);
        ShowSaveButton();
    }

    public void DisableVibro()
    {
        _offVibro.SetActive(true);
        _onVibro.SetActive(false);
        _vibration = 0;
        PlayerPrefs.SetFloat("VibroStatus", _vibration);
        ShowSaveButton();
    }

    public void EnableVibro()
    {
        _onVibro.SetActive(true);
        _offVibro.SetActive(false);
        _vibration = 1;
        PlayerPrefs.SetFloat("VibroStatus", _vibration);
        ShowSaveButton();
    }

    public void PlayClickSound()
    {
        if (_vibration == 1) Vibration.VibratePop();
        _soundController.PlayOneShot(_clickSound);
    }

    public void ClearAllData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene("Scene_Menu");
    }

    private void ShowSaveButton()
    {
        _backBtn.SetActive(false);
        _saveBtn.SetActive(true);
    }

    public void ShowBackButton()
    {
        _saveBtn.SetActive(false);
        _backBtn.SetActive(true);
    }

    public void PlayChickSound()
    {
        if (_vibration == 1) Vibration.VibratePop();
        _soundController.PlayOneShot(_tic);
    }

    public void PlayCorretcSound()
    {
        if (_vibration == 1) Vibration.VibratePeek();
        _soundController.PlayOneShot(_correct);
    }

    public void DisableBGMusic()
    {
        _musicController.Stop();
    }

    public void PlayWinSound()
    {
        if (_vibration == 1) Vibration.Vibrate();
        _soundController.PlayOneShot(_win);
    }

    public void PlayEndTimeSound()
    {
        if (_vibration == 1) Vibration.Vibrate();
        _soundController.PlayOneShot(_endTime);
    }

    public void PlayWrongSound()
    {
        if (_vibration == 1) Vibration.Vibrate();
        _soundController.PlayOneShot(_wrong);
    }

    public void SwithThemeToLight()
    {
        _darkTheme.SetActive(false);
        _lightTheme.SetActive(true);
        _themeIndex = 0;
        foreach (var theme in _themesImages)
        {
            theme.sprite = _themesSprites[_themeIndex];
        }

        PlayerPrefs.SetInt("ThemeIndex", _themeIndex);
        ShowSaveButton();
    }

    public void SwithThemeToDark()
    {
        _lightTheme.SetActive(false);
        _darkTheme.SetActive(true);
        _themeIndex = 1;
        foreach (var theme in _themesImages)
        {
            theme.sprite = _themesSprites[_themeIndex];
        }

        PlayerPrefs.SetInt("ThemeIndex", _themeIndex);
        ShowSaveButton();
    }
}