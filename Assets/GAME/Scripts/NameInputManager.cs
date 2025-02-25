using UnityEngine;
using UnityEngine.UI;

public class NameInputManager : MonoBehaviour
{
    public InputField[] nameFields; // 6 полей ввода имен
    public Button[] addButtons; // 4 кнопки "Add"

    private void Start()
    {
        // Загружаем сохраненные имена из PlayerPrefs
        for (int i = 0; i < nameFields.Length; i++)
        {
            string savedName = PlayerPrefs.GetString("PlayerName" + i, "");
            nameFields[i].text = savedName;
        }

        // Скрываем дополнительные поля (начиная с 3-го) и кнопки Add (кроме первой)
        for (int i = 2; i < nameFields.Length; i++)
        {
            nameFields[i].gameObject.SetActive(false);
        }
        for (int i = 1; i < addButtons.Length; i++)
        {
            addButtons[i].gameObject.SetActive(false);
        }
    }

    // Метод для добавления нового поля
    public void AddNameField(int index)
    {
        if (index < nameFields.Length)
        {
            nameFields[index].gameObject.SetActive(true); // Включаем поле
            addButtons[index - 2].gameObject.SetActive(false); // Прячем текущую кнопку
        }

        if (index - 1 < addButtons.Length)
        {
            addButtons[index - 1].gameObject.SetActive(true); // Включаем следующую кнопку
        }
    }

    // Метод для сохранения введенных имен в PlayerPrefs
    public void SaveNames()
    {
        for (int i = 0; i < nameFields.Length; i++)
        {
            PlayerPrefs.SetString("PlayerName" + i, nameFields[i].text);
        }
        PlayerPrefs.Save();
    }

    public void ResetToDefaultState()
    {
        // Отключаем все дополнительные поля
        for (int i = 2; i < nameFields.Length; i++)
        {
            nameFields[i].gameObject.SetActive(false);
        }

        // Включаем первые два поля
        for (int i = 0; i < 2; i++)
        {
            nameFields[i].gameObject.SetActive(true);
        }

        // Прячем все кнопки "Add" кроме первой
        for (int i = 1; i < addButtons.Length; i++)
        {
            addButtons[i].gameObject.SetActive(false);
        }

        // Включаем первую кнопку "Add"
        addButtons[0].gameObject.SetActive(true);
    }

}
