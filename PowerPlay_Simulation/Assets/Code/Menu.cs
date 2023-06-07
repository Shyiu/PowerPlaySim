using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{

    public GameObject loadingScreen;
    public TMP_Text progressText;

    public void playGame(int sceneIndex)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(loadAsynchronously(sceneIndex));
    }

    IEnumerator loadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        string text = "Loading . . . ";

        while (!operation.isDone)
        {
           
            progressText.text = text;
            Debug.Log(operation.progress);
      

            yield return null;

        }
    }
    public void buttonClick()
    {
        AudioSource a = GameObject.Find("ButtonClick").GetComponent<AudioSource>();
        a.Play(0);
    }
}
