using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunctionDetection : MonoBehaviour
{
    GameObject robot;
    GameObject blueCone;
    MeshRenderer meshRendererObj;
    Ray ray;
    RaycastHit hit;
    bool emissionToggle = false;
    public float distance = 2.5f;
    Detection d;
    public float heightConstant = 3.5f;
    public float coneLimit = 8;
    private int conesPlaced = 0;
    // Start is called before the first frame update
    void Start()
    {
        robot = GameObject.Find("Robot1");
        blueCone = GameObject.Find("Blue_Cone");
        meshRendererObj = GetComponent<MeshRenderer>();
        d = robot.GetComponent<Detection>();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Rigidbody>().velocity.y == 0)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX |RigidbodyConstraints.FreezePositionZ |RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        ray = new Ray(robot.transform.position, robot.transform.forward);
        if(Physics.Raycast(ray, out hit, distance) && hit.collider.name.Equals(gameObject.name) && conesPlaced < coneLimit && !d.canPickupCone()){
            for (int i = 0; i < meshRendererObj.materials.Length; i++)
                {
                    meshRendererObj.materials[i].EnableKeyword("_EMISSION");
                }
            emissionToggle = true;
        }
        else{
            for (int i = 0; i < meshRendererObj.materials.Length; i++)
                {
                    meshRendererObj.materials[i].DisableKeyword("_EMISSION");
                }
                emissionToggle = false;
        }
        if(emissionToggle){
            if(Input.GetKeyDown(KeyCode.Alpha1) && !d.canPickupCone() && conesPlaced < coneLimit){
                d.scoreCone();
                conesPlaced += 1;
                GameObject newBlueCone = Instantiate(blueCone, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + heightConstant, gameObject.transform.position.z), Quaternion.identity);
                newBlueCone.gameObject.transform.localScale += new Vector3(9,9,9);
                Rigidbody blueConeRb =  newBlueCone.GetComponent<Rigidbody>();
                blueConeRb.mass = 625;
                blueConeRb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ| RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationY;
            
            }
        }
    }
}
