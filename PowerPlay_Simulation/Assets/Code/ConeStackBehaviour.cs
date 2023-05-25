using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeStackBehaviour : MonoBehaviour
{
    private RaycastHit hit;
    private Ray ray;
    private List<Collider> cones = new List<Collider>();
    private int conesLeft = 5;
    private bool complete = false;
    public float coneDistance = 1.5f;
    [SerializeField] GameObject robot;
    private bool lightOn = false;
    private bool sort = false;
    private Detection robotScript;
    void Start()
    {
        robotScript = robot.GetComponent<Detection>();        

    }
  
    // Update is called once per frame
     
    void OnTriggerEnter(Collider c){
        Debug.Log("Trigger has been entered");
        if(c.gameObject.name.Contains("Blue_Cone")){
            complete = false;
            // for(int x = 0; x < cones.Count; x++){
            //     if(cones[x].gameObject.transform.position.y >= c.gameObject.transform.position.y){
            //         cones.Insert(x, c);
            //         complete = true;
            //     }
            // }
            cones.Insert(0, c);
            // if(!complete){
            //     cones.Add(c);
            // }
            
            
        }
    }
    private void lightUpCone(){
        MeshRenderer meshRendererObj = cones[conesLeft - 1].gameObject.GetComponent<MeshRenderer>();
                for (int i = 0; i < meshRendererObj.materials.Length;i++)
                {
                meshRendererObj.materials[i].EnableKeyword("_EMISSION");
                }   
    }
    private void darkenCone(){
        for(int j = 0; j < cones.Count; j++){
        MeshRenderer meshRendererObj = cones[j].gameObject.GetComponent<MeshRenderer>();
                for (int i = 0; i < meshRendererObj.materials.Length;i++)
                {
                meshRendererObj.materials[i].DisableKeyword("_EMISSION");
                }   
        }
    }
    private static int CompareYValues(Collider c1, Collider c2){
        if(c1 == null){
            if(c2 == null){
                return 0;
            }
            return (int) (c2.gameObject.transform.position.y * 10);
        }
        else {
            if(c2 == null){
                return (int) (c1.gameObject.transform.position.y * 10);
            }
        }
        if(c1.gameObject.transform.position.y * 10 > c2.gameObject.transform.position.y * 10){
            return (int) (c1.gameObject.transform.position.y * 10);
        }
        return (int) (c2.gameObject.transform.position.y * 10);
    }
    private void destroyTopCone(){
        Destroy(cones[conesLeft-1].gameObject);
        cones.RemoveAt(conesLeft - 1);
        conesLeft -= 1;

    }
    void Update()
    {
        if(!sort){
            cones.Sort(CompareYValues);
            sort = true;
        }
        Debug.Log(conesLeft);
        if(conesLeft != 0){
         ray = new Ray(robot.transform.position, robot.transform.forward);
         if (Physics.Raycast(ray, out hit, coneDistance) && hit.collider.name.Equals(gameObject.name)){
                lightUpCone();
                lightOn = true;
         } 
         else if(lightOn){
            darkenCone();
            lightOn = false;
         }
         if(lightOn && Input.GetKeyDown(KeyCode.Alpha2) && robotScript.canPickupCone()){
            robotScript.pickUpCone();
            destroyTopCone();
            
         }
        }

    }  
}
