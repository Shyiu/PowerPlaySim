using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBoard : MonoBehaviour
{   
    private int[] blueCones = {0,0,0,0};
    private int[] redCones = {0,0,0,0};
    [SerializeField] GameObject[] blueTextBoxes;
    [SerializeField] GameObject[] redTextBoxes;
    [SerializeField] GameObject redScore;
    [SerializeField] GameObject blueScore;
    TextMeshProUGUI blueScoreText;
    TextMeshProUGUI redScoreText;
    TextMeshProUGUI[] blueTexts;
    TextMeshProUGUI[] redTexts;
    int redScoreValue = 0;
    int blueScoreValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        blueTexts = new TextMeshProUGUI[4];
        redTexts = new TextMeshProUGUI[4];
        blueScoreText = blueScore.GetComponent<TextMeshProUGUI>();
        redScoreText = redScore.GetComponent<TextMeshProUGUI>();

        for(int i = 0; i < blueTextBoxes.Length; i++){
            blueTexts[i] = blueTextBoxes[i].GetComponent<TextMeshProUGUI>();
        }
        for(int i = 0; i < redTextBoxes.Length; i++){
            redTexts[i] = redTextBoxes[i].GetComponent<TextMeshProUGUI>();
        }
    }
    public void placeBlueCone(int position){
        blueCones[position] += 1;
        blueTexts[position].text = blueCones[position].ToString();
        blueScoreValue += 2 + position;
        blueScoreText.text = blueScoreValue.ToString();
    }
    public void placeRedCone(int position){
        redCones[position] += 1;
        redTexts[position].text = redCones[position].ToString();
        redScoreValue += 2 + position;
        redScoreText.text = redScoreValue.ToString();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
