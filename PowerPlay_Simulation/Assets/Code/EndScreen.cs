using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndScreen : MonoBehaviour
{
    public TMP_Text text;
    int redTotalScore = 0;
    int blueTotalScore = 0;

    

    private void Update()
    {
        if (redTotalScore > blueTotalScore)
        {
            text.text = "The Red Player Won!";
        } else if (blueTotalScore > redTotalScore)
        {
            text.text = "The Blue Player Won!";
        } else
        {
            text.text = "It was a draw!";
        }
    }

    void OnEnable()
    {
        redTotalScore = PlayerPrefs.GetInt("redScore");
        blueTotalScore = PlayerPrefs.GetInt("blueScore");
    }

    public void playGame()
    {
        SceneManager.LoadScene(0);
    }
}
