using UnityEngine;

public class LevelChooser : MonoBehaviour
{
    public void ChooseLevel(int levelIndex)
    {
        PlayerPrefs.SetInt("LevelType", levelIndex);
    }
}