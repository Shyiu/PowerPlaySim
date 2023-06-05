using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DebugMode : MonoBehaviour
{
    public bool autoCones = false;
    public bool cursorSelection = false;
    private Detection robot1;
    private Detection robot2;
    private List<JunctionDetection> junctionScripts = new List<JunctionDetection>();
    private string[] letters = {"A", "B", "C", "D", "E"};
    private int[] numbers = {1,2,3,4,5};
    private string[] options = {"Ground", "Short", "Medium", "High"};
    // Start is called before the first frame update
    void Start()
    {
        robot1 = GameObject.Find("Robot1").GetComponent<Detection>();
        robot2 = GameObject.Find("Robot2").GetComponent<Detection>();
        string letter;
        string number;
        string type;
        foreach(string s in letters){
            foreach(int n in numbers){
                foreach(string o in options){
                    letter = s;
                    number = n + "";
                    type = o;
                    try{
                        junctionScripts.Add(GameObject.Find(type + letter + number).GetComponent<JunctionDetection>());
                    }
                    catch(Exception e){
                        //Debug.Log(e);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(autoCones){
            if(robot1.canPickupCone()){
                robot1.pickUpCone();
            }
            if(robot2.canPickupCone()){
                robot2.pickUpCone();
            }
        }
        if(cursorSelection){
            GameObject.Find("Wall2").GetComponent<Collider>().enabled = false;
            GameObject.Find("Wall1").GetComponent<Collider>().enabled = false;
            GameObject.Find("Wall4").GetComponent<Collider>().enabled = false;
            GameObject.Find("Wall3").GetComponent<Collider>().enabled = false;
            foreach(JunctionDetection j in junctionScripts){
                j.enableMouse();
            }
        }
        else{
            GameObject.Find("Wall1").GetComponent<Collider>().enabled = true;
            GameObject.Find("Wall4").GetComponent<Collider>().enabled = true;
            GameObject.Find("Wall2").GetComponent<Collider>().enabled = true;
            GameObject.Find("Wall3").GetComponent<Collider>().enabled = true;
            foreach(JunctionDetection j in junctionScripts){
                j.disableMouse();
            }
        }
    }
}
