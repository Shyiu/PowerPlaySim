using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{

    public GameObject loadingScreen;
    public Slider slider;
    public TMP_Text progressText;

    public void playGame(int sceneIndex)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(loadAsynchronously(sceneIndex));
    }

    IEnumerator loadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        

        while (!operation.isDone)
        {
            //float progress = Mathf.Clamp01(operation.progress / 0.9f);
            float progress = operation.progress;
            progressText.text = progress * 100f + "%";
            Debug.Log(operation.progress);
            slider.value = progress;

            yield return null;

        }
    }
}
