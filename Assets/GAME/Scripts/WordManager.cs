using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class WordCategory
{
    public string categoryName; // Название категории (необязательно)
    public List<string> words = new List<string>(); // Список слов
}

public class WordManager : MonoBehaviour
{
    public List<WordCategory> categories = new List<WordCategory>(); // 9 категорий

    
    public string GetRandomWord(int categoryIndex)
    {
        if (categoryIndex >= 0 && categoryIndex < categories.Count)
        {
            WordCategory category = categories[categoryIndex];
            if (category.words.Count > 0)
            {
                return category.words[Random.Range(0, category.words.Count)];
            }
        }
        return "No words found!";
    }
}
