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
    int blueCircuitValue = 0;
    int redCircuitValue = 0;
    bool terminalTopLeft = true;
    bool terminalTopRight = true;
    bool terminalBottomLeft = true;
    bool terminalBottomRight = true;
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
        calculateCircuitBonus();
        blueScoreText.text = (blueScoreValue +blueControlValue + blueCircuitValue).ToString();
        redScoreText.text = (redScoreValue + redControlValue + redCircuitValue).ToString();
    }
    public void calculateCircuitBonus(){
        if(terminalTopLeft && terminalBottomRight && pathfinder.isPath(control, 1, true)){
            blueCircuitValue = 20;
        }
        else{
            blueCircuitValue = 0;
        }
        if(terminalBottomLeft && terminalTopRight && pathfinder.isPath(control, 2, false)){
            redCircuitValue = 20;
        }
        else{
            redCircuitValue = 0;
        }
    }
    public void displayMap(int[,] map){
        int row = map.Length/map.GetLength(0);
        int col = map.GetLength(0);
        string current = "";
        for(int r = 0; r < row; r++){
            for(int c = 0; c < col; c++){
                current += map[r,c] + " ";
            }
            current += "\n";
        }
    }
    
    public void placeBlueCone(int position, float row, float col){
        blueCones[position] += 1;
        blueTexts[position].text = blueCones[position].ToString();
        blueScoreValue += 2 + position;
        GameObject blueCircle = Instantiate(GameObject.Find("BlueFilledCircle"), new Vector3(startx - step*col, starty + step*row, 0), Quaternion.identity);
        blueCircle.transform.SetParent(GameObject.Find("ScoreBoardImage").transform, false);
        control[(int) (-row + 2), (int)col + 2] = 1;
        updateScore();
        
        
    }
    public void placeRedCone(int position, float row, float col){
        redCones[position] += 1;
        redTexts[position].text = redCones[position].ToString();
        redScoreValue += 2 + position;
        GameObject redCircle = Instantiate(GameObject.Find("RedFilledCircle"), new Vector3(startx - step*col, starty + step*row, 0), Quaternion.identity);
        redCircle.transform.SetParent(GameObject.Find("ScoreBoardImage").transform, false);
        control[(int) (-row + 2), (int)col + 2] = 2;
        updateScore();
        // redCircle.transform.position = new Vector3(startx + step*col,  starty + step*row, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
 
class pair {
  public int Item1, Item2;
  public pair(int f, int s)
  {
    Item1 = f;
    Item2 = s;
  }
}
 
class pathfinder {
 
  static int row;
  static int col;
  static int row2;
  static int col2;
  static int row3;
  static int col3;
  static public int[,] cloneArray(int[,] original){
        int[,] output = new int[original.Length/original.GetLength(0), original.GetLength(1)];
        for(int r = 0; r < original.Length/original.GetLength(0); r++){
            for(int c = 0; c < original.GetLength(0); c++){
                output[r,c] = original[r,c];
            }
        }
        return output;
    }
  // To find the path from
  // top left to bottom right
  public static void displayMap(int[,] map){
        int row = map.Length/map.GetLength(0);
        int col = map.GetLength(0);
        string current = "";

        for(int r = 0; r < row; r++){
            for(int c = 0; c < col; c++){
                current += map[r,c] + " ";
            }
            current += "\n";
        }
        Debug.Log(current);
    }
    
  public static bool isPath(int[, ] arr, int pathValue, bool direction)
  {
    int[,] newarr = cloneArray(arr);
    displayMap(newarr);
    LinkedList<pair> q = new LinkedList<pair>();
    if(direction && (arr[0,0] == 1 || arr[1,0] == 1 || arr[0,1] == 1)){
        if(arr[0,0] == 1){
            q.AddLast(new pair(0, 0));
        }
        else if(arr[0,1] == 1){
            q.AddLast(new pair(0, 1));
        }
        else if(arr[1,0] == 1){
            q.AddLast(new pair(1, 0));
        }
        
        row = 5;
        col = 5;
        row2 = 4;
        col2 = 5;
        row3 = 5;
        col3 = 4;
    }
    else if (arr[0,4] == 2 || arr[1,4] == 2 || arr[0,3] == 2){
        if(arr[0,4] == 2){
            q.AddLast(new pair(0, 4));
        }
        else if(arr[1,4] == 2){
            q.AddLast(new pair(1, 4));
        }
        else if(arr[0,3] == 2){
            q.AddLast(new pair(0, 3));
        }
        row = 5;
        col = 1;
        row2 = 4;
        col2 = 1;
        row3 = 5;
        col3 = 2;
    }
    else{
        return false;
    }
    // Directions
    int[, ] dir
      = { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 }, { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } };
 
    // Queue
    
    // Insert the top right corner.
    
 
    // Until queue is empty
    while (q.Count > 0) {
      pair p = (pair)(q.First.Value);
      q.RemoveFirst();
 
      // Mark as visited
      newarr[p.Item1, p.Item2] = 0;
 
      // Destination is reached.
      if (p.Item1 == row - 1 && p.Item2 == col - 1)
        return true;
 
      // Check all four directions
      for (int i = 0; i < 8; i++) {
 
        // Using the direction array
        int a = p.Item1 + dir[i, 0];
        int b = p.Item2 + dir[i, 1];
 
        // Not blocked and valid
        if (a >= 0 && b >= 0 && a < 5 && b < 5
            && newarr[a, b] == pathValue) {
          if (a == row - 1 && b == col - 1)
            return true;
        if(a == row2 - 1 && b == col2 - 1){
            return true;
        }
        if(a == row3 - 1 && b == col3 - 1){
            return true;
        }
 
          q.AddLast(new pair(a, b));
        }
      }
    }
    return false;
  }
}
