using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private int x;
    private int z;
    [SerializeField] Collider c;
    private GameObject robot1;
    private GameObject robot2;
    private GameObject blueCone;
    private GameObject redCone;
    Detection d;
    Detection d2;
    ScoreBoard s;
    int count = 0;
    private bool toggle = false;
    private bool blue = false;
    RaycastHit hit;
    void Start()
    {
        if(gameObject.name.Contains("TopLeft")){
            x = -11;
            z = 11;
            blue = true;
        }
        else if(gameObject.name.Contains("TopRight")){
            x = 11;
            z = 11;
        }
        else if(gameObject.name.Contains("BottomRight")){
            x = 11;
            z = -11;
            blue = true;
        }
        else if(gameObject.name.Contains("BottomLeft")){
            x = -11;
            z = -11;
        }
        robot1 = GameObject.Find("Robot1");
        blueCone = GameObject.Find("Blue_Cone_Sample");
        robot2 = GameObject.Find("Robot2");
        redCone = GameObject.Find("Red_Cone_Sample");
        d = robot1.GetComponent<Detection>();
        d2 = robot2.GetComponent<Detection>();
        s = GameObject.Find("Canvas").GetComponent<ScoreBoard>();
    }

    // Update is called once per frame
    void Update()
    {
        if(blue){
            Ray ray = new Ray(robot1.transform.position, robot1.transform.forward);
            if(Physics.Raycast(ray, out hit, 2.5f) && hit.collider.name.Equals(c.gameObject.name) && !d.canPickupCone()){
                for (int i = 0; i < GetComponent<MeshRenderer>().materials.Length; i++)
                {
                    GetComponent<MeshRenderer>().materials[i].EnableKeyword("_EMISSION");
                    toggle = true;
                }
            }
            else{
                for (int i = 0; i < GetComponent<MeshRenderer>().materials.Length; i++)
                {
                    GetComponent<MeshRenderer>().materials[i].DisableKeyword("_EMISSION");
                    toggle = false;
                }
            }
            if (toggle){
                if((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) && !d.canPickupCone()){
                    d.scoreCone();
                    GameObject newBlueCone = Instantiate(blueCone, new Vector3(x , 4f, z), Quaternion.identity);
                    newBlueCone.gameObject.transform.localScale += new Vector3(9,9,9);
                    Rigidbody blueConeRb =  newBlueCone.GetComponent<Rigidbody>();
                    blueConeRb.mass = 625;
                    newBlueCone.gameObject.name = gameObject.name + "TerminalCone" + count;
                    count+=1;
                    blueConeRb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ| RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationY;
                    if(x == -11 && z == 11){
                        s.placeBlueCone(0);
                    }
                    else{
                        s.placeBlueCone(1);
                    }
                }
            }
        }
        else{
            Ray ray = new Ray(robot2.transform.position, robot2.transform.forward);
            if(Physics.Raycast(ray, out hit, 2.5f) && hit.collider.name.Equals(c.gameObject.name) && !d.canPickupCone()){
                for (int i = 0; i < GetComponent<MeshRenderer>().materials.Length; i++)
                {
                    GetComponent<MeshRenderer>().materials[i].EnableKeyword("_EMISSION");
                    toggle = true;
                }
            }
            else{
                for (int i = 0; i < GetComponent<MeshRenderer>().materials.Length; i++)
                {
                    GetComponent<MeshRenderer>().materials[i].DisableKeyword("_EMISSION");
                    toggle = false;
                }
            }
            if (toggle){
                if((Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.O)) && !d2.canPickupCone()){
                    d2.scoreCone();
                    GameObject newRedCone = Instantiate(redCone, new Vector3(x , 4f, z), Quaternion.identity);
                    newRedCone.gameObject.transform.localScale += new Vector3(9, 9, 9);
                    Rigidbody RedConeRb = newRedCone.GetComponent<Rigidbody>();
                    RedConeRb.mass = 625;
                    count+=1;
                    RedConeRb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                    if(x == 11 && z == 11){
                        s.placeRedCone(0);
                    }
                    else{
                        s.placeRedCone(1);
                    }
                    
                }
            }
        }
    }
}
