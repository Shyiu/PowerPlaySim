using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedConesBehaviour :MonoBehaviour
{
    private int redConesLeft = 20;
    private int blueConesLeft = 20;
    private bool redConePlaced = false;
    private bool blueConePlaced = false;
    private float cooldown = 5;
    private float redCurrentTime = -10;
    private float blueCurrentTime = -10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void placeRedCone(){
        string topCone = "Red Cone " + (31 - redConesLeft);
        GameObject cone = GameObject.Find(topCone);
        GameObject coneClone = Instantiate(cone, new Vector3(0, 0.05f, 11.25f), Quaternion.identity);
        Rigidbody coneRb =  coneClone.GetComponent<Rigidbody>();
        coneRb.useGravity = false;
        coneRb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ| RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationY;
        MeshRenderer blueConeCollider = coneClone.GetComponent<MeshRenderer>();
        blueConeCollider.material = GameObject.Find("Red_Cone_Sample").GetComponent<MeshRenderer>().material;
        redConesLeft -= 1;
        Destroy(cone);
    }
    void placeBlueCone(){
        string topCone = "Blue Cone " + (31 - blueConesLeft);
        GameObject cone = GameObject.Find(topCone);
        GameObject coneClone = Instantiate(cone, new Vector3(0, 0.05f, -11.25f), Quaternion.identity);
        Rigidbody coneRb =  coneClone.GetComponent<Rigidbody>();
        coneRb.useGravity = false;
        coneRb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        MeshRenderer blueConeCollider = coneClone.GetComponent<MeshRenderer>();
        blueConeCollider.material = GameObject.Find("Blue_Cone_Sample").GetComponent<MeshRenderer>().material;
        blueConesLeft -= 1;
        Destroy(cone);
    }
    public void pickUpRedCone(){
        redConePlaced = false;
        redCurrentTime = Time.realtimeSinceStartup;

    }
    public void pickUpBlueCone(){
        blueConePlaced = false;
        blueCurrentTime = Time.realtimeSinceStartup;
    }
    void Update()
    {
        if(!redConePlaced && Time.realtimeSinceStartup - redCurrentTime > cooldown && redConesLeft != 0){
            placeRedCone();
            redConePlaced = true;
        }
        if(!blueConePlaced && Time.realtimeSinceStartup - blueCurrentTime > cooldown && blueConesLeft != 0){
            placeBlueCone();
            blueConePlaced = true;
        }
    }
}
