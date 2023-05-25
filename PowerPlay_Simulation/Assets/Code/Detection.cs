using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO: Add a method where everytime a cone is place, a box collider is created that is as high as the base of the cone. 
public class Detection : MonoBehaviour
{
    GameObject obj;
    MeshRenderer meshRendererObj;
    Vector3 hostPos;
    Vector3 direction;
    Ray ray;
    GameObject priorObj;
    GameObject priorCone;
    RaycastHit hit;
    // private int coneId;
    [SerializeField] GameObject Robot;
    [SerializeField] GameObject blueCone;
    public float distance = 2.5f;
    public float coneDistance = .5f;
    private bool conePickedUp;
    ConeStackBehaviour rightSide;
    private bool stackCone;
    private bool glowTarget;
    // Start is called before the first frame update
    void Start()
    {

        obj = Robot;
        meshRendererObj = obj.GetComponent<MeshRenderer>();
        priorObj = Robot;
        conePickedUp = false;        
    }


    // Update is called once per frame
        
    public bool canPickupCone(){
        return !conePickedUp;
    }
    public void scoreCone(){
        conePickedUp = false;
    }
    public void pickUpCone(){
        conePickedUp = true;
    }
    void Update()
{
  
       
    }

}