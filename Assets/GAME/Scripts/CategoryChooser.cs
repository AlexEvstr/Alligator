using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryChooser : MonoBehaviour
{
    public void ChooseCatogory(int categoryIndex)
    {
        PlayerPrefs.SetInt("CategoryType", categoryIndex);
    }
}
