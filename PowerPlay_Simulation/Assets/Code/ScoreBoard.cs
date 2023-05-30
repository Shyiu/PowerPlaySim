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
    int blueControlValue = 0;
    int redControlValue = 0;
    private int startx = 0;
    private int starty = 19;
    private int step = 22;
    private int[,] control = new int[5,5];

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
    public void calculateControlValues(){
        blueControlValue = 0;
        redControlValue = 0;
        for(int r = 0; r < 5; r++){
            for(int c= 0; c < 5; c++){
                if(control[r,c] == 1){
                    blueControlValue += 3;
                }
                if(control[r,c] == 2){
                    redControlValue += 3;
                }
            }
        }
    }
    public void updateScore(){
        calculateControlValues();
        blueScoreText.text = (blueScoreValue +blueControlValue).ToString();
         redScoreText.text = (redScoreValue + redControlValue).ToString();
    }
    public void placeBlueCone(int position, float row, float col){
        blueCones[position] += 1;
        blueTexts[position].text = blueCones[position].ToString();
        blueScoreValue += 2 + position;
        GameObject blueCircle = Instantiate(GameObject.Find("BlueFilledCircle"), new Vector3(startx - step*col, starty + step*row, 0), Quaternion.identity);
        blueCircle.transform.SetParent(GameObject.Find("ScoreBoardImage").transform, false);
        control[(int) -col + 2, (int)row + 2] = 1;
        updateScore();
        
        
    }
    public void placeRedCone(int position, float row, float col){
        redCones[position] += 1;
        redTexts[position].text = redCones[position].ToString();
        redScoreValue += 2 + position;
        GameObject redCircle = Instantiate(GameObject.Find("RedFilledCircle"), new Vector3(startx - step*col, starty + step*row, 0), Quaternion.identity);
        redCircle.transform.SetParent(GameObject.Find("ScoreBoardImage").transform, false);
        control[(int) -col + 2, (int)row + 2] = 2;
        updateScore();
        // redCircle.transform.position = new Vector3(startx + step*col,  starty + step*row, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
