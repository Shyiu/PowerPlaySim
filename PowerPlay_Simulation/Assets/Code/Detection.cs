using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO: Add a method where everytime a cone is place, a box collider is created that is as high as the base of the cone. 
public class Detection : MonoBehaviour
{
    private bool conePickedUp;

    // Start is called before the first frame update
    void Start()
    {
        conePickedUp = false;        
    }


    // Update is called once per frame
        
    public bool canPickupCone(){
        return !conePickedUp;
    }
    public void scoreCone(){
        conePickedUp = false;
        gameObject.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");

    }
    public void pickUpCone(){
        conePickedUp = true;
        gameObject.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
    }
    void Update()
{
       
    }

}