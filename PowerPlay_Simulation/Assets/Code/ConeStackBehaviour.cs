using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeStackBehaviour : MonoBehaviour
{
    private RaycastHit hit;
    private Ray ray;
    private List<Collider> cones = new List<Collider>();
    private int conesLeft = 5;
    public float coneDistance = 1.5f;
    [SerializeField] GameObject robot;
    [SerializeField] GameObject robot2;
    private bool lightOn = false;
    private bool sort = false;
    private bool blue = false;
    private Detection robotScript;
    private Detection robotScript2;
    private bool retrieved = false;
    void Start()
    {
        robotScript = robot.GetComponent<Detection>();
        robotScript2 = robot2.GetComponent<Detection>();
       

    }

    // Update is called once per frame

    private void lightUpCone(){
        if(cones[conesLeft - 1] == null){
            return;
        }
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
        if (GameObject.Find("Floor").GetComponent<BarrierGeneration>().ready() && !retrieved)
        {
            cones = GameObject.Find("Floor").GetComponent<BarrierGeneration>().getCones(gameObject.name);
            if (gameObject.name.Contains("Red"))
            {
                blue = false;
            }
            else
            {
                blue = true;
            }

        }
        if (conesLeft != 0)
        {
            if (blue)
            {
                ray = new Ray(robot.transform.position, robot.transform.forward);
                if (Physics.Raycast(ray, out hit, coneDistance) && hit.collider.name.Equals(gameObject.name) && blue)
                {
                    lightUpCone();
                    lightOn = true;
                }
                else if (lightOn)
                {
                    darkenCone();
                    lightOn = false;
                }
                
            }
            else {
                ray = new Ray(robot2.transform.position, robot2.transform.forward);
                if (Physics.Raycast(ray, out hit, coneDistance) && hit.collider.name.Equals(gameObject.name) && !blue)
            {
                lightUpCone();
                lightOn = true;
            }
            else if (lightOn)
            {
                darkenCone();
                lightOn = false;
            }
        }
         if(lightOn && (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) && robotScript.canPickupCone() && blue)
            {
                robotScript.pickUpCone();
                destroyTopCone();
                
            }
         if (lightOn && (Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.O)) && robotScript2.canPickupCone() && !blue)
            {
                robotScript2.pickUpCone();
                destroyTopCone();

            }
        }

    }  
}
