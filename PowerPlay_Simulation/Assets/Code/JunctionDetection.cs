using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunctionDetection : MonoBehaviour
{
    GameObject robot;
    GameObject robot2;
    GameObject blueCone;
    GameObject redCone;
    MeshRenderer meshRendererObj;
    Ray ray;
    RaycastHit hit;
    bool emissionToggle = false;
    bool emissionToggle2 = false;
    public float distance = 2.5f;
    Detection d;
    Detection d2;
    public float heightConstant = 3.5f;
    public float coneLimit = 8;
    private int conesPlaced = 0;
    // Start is called before the first frame update
    void Start()
    {
        robot = GameObject.Find("Robot1");
        blueCone = GameObject.Find("Blue_Cone");
        robot2 = GameObject.Find("Robot2");
        redCone = GameObject.Find("Red_Cone");
        meshRendererObj = GetComponent<MeshRenderer>();
        d = robot.GetComponent<Detection>();
        d2 = robot2.GetComponent<Detection>();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Rigidbody>().velocity.y == 0)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX |RigidbodyConstraints.FreezePositionZ |RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        }
        ray = new Ray(robot.transform.position, robot.transform.forward);
        Ray ray2 = new Ray(robot2.transform.position, robot2.transform.forward);
        if (Physics.Raycast(ray, out hit, distance) && hit.collider.name.Equals(gameObject.name) && conesPlaced < coneLimit && !d.canPickupCone()){
            for (int i = 0; i < meshRendererObj.materials.Length; i++)
                {
                    meshRendererObj.materials[i].EnableKeyword("_EMISSION");
                }
            emissionToggle = true;
        }
        else if(Physics.Raycast(ray2, out hit, distance) && hit.collider.name.Equals(gameObject.name) && conesPlaced < coneLimit && !d2.canPickupCone()){
            for (int i = 0; i < meshRendererObj.materials.Length; i++)
            {
                meshRendererObj.materials[i].EnableKeyword("_EMISSION");
            }
            emissionToggle2 = true;
        }
        else
        {
            for (int i = 0; i < meshRendererObj.materials.Length; i++)
                {
                    meshRendererObj.materials[i].DisableKeyword("_EMISSION");
                }
                emissionToggle = false;
                emissionToggle2 = false;
        }
        
      
        if (emissionToggle){
            if((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) && !d.canPickupCone() && conesPlaced < coneLimit){
                d.scoreCone();
                conesPlaced += 1;
                GameObject newBlueCone = Instantiate(blueCone, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + heightConstant, gameObject.transform.position.z), Quaternion.identity);
                newBlueCone.gameObject.transform.localScale += new Vector3(9,9,9);
                Rigidbody blueConeRb =  newBlueCone.GetComponent<Rigidbody>();
                blueConeRb.mass = 625;
                blueConeRb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ| RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationY;
            
            }
        }
        if (emissionToggle2)
        {
            if ((Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.O)) && !d2.canPickupCone() && conesPlaced < coneLimit)
            {
                d2.scoreCone();
                conesPlaced += 1;
                GameObject newRedCone = Instantiate(redCone, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + heightConstant, gameObject.transform.position.z), Quaternion.identity);
                newRedCone.gameObject.transform.localScale += new Vector3(9, 9, 9);
                Rigidbody RedConeRb = newRedCone.GetComponent<Rigidbody>();
                RedConeRb.mass = 625;
                RedConeRb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

            }
        }
    }
}
