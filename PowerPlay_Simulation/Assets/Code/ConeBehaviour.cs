using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float offset = 4f;
    private float ypos = 0;
    private Ray ray;
    private Ray ray2;
    private RaycastHit hit;
    private GameObject robot;
    private GameObject robot2;
    private Detection robotScript;
    private Detection robotScript2;
    private bool coneStackCone = false;
    private bool lightOn = false;
    private bool blue = false;
    private float coneDistance = 1.5f;
    PlacedConesBehaviour p;
    void Start()
    {
        if(gameObject.name.Contains("Red")){
            blue = false;
        }
        else{
            blue = true;
        }
        robot = GameObject.Find("Robot1");
        robot2 = GameObject.Find("Robot2");
        robotScript = robot.GetComponent<Detection>();
        robotScript2 = robot2.GetComponent<Detection>();
        if(gameObject.name.Contains("Stack")){
            coneStackCone = true;
        }
        ypos = transform.position.y;
        p = GameObject.Find("Floor").GetComponent<PlacedConesBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Rigidbody>().mass < 600){
        transform.position = new Vector3(transform.position.x, ypos, transform.position.z);
        }
        if(transform.position.y < 0){
            transform.position = new Vector3(transform.position.x, 0.05f, transform.position.z);
        }
        if(!coneStackCone && GetComponent<Rigidbody>().mass < 600){
            ray = new Ray(robot.transform.position, robot.transform.forward);
            ray2 = new Ray(robot2.transform.position, robot2.transform.forward);
            if(Physics.Raycast(ray, out hit, coneDistance) && hit.collider.name.Equals(gameObject.name)){
                MeshRenderer meshRendererObj = gameObject.GetComponent<MeshRenderer>();
                for (int i = 0; i < meshRendererObj.materials.Length;i++)
                {
                meshRendererObj.materials[i].EnableKeyword("_EMISSION");
                
                }   
                lightOn = true;
            }
            else if(Physics.Raycast(ray2, out hit, coneDistance) && hit.collider.name.Equals(gameObject.name)){
                MeshRenderer meshRendererObj = gameObject.GetComponent<MeshRenderer>();
                for (int i = 0; i < meshRendererObj.materials.Length;i++)
                {
                meshRendererObj.materials[i].EnableKeyword("_EMISSION");
                }  
                lightOn = true;
            }
            else{
                MeshRenderer meshRendererObj = gameObject.GetComponent<MeshRenderer>();
                for (int i = 0; i < meshRendererObj.materials.Length;i++)
                {
                meshRendererObj.materials[i].DisableKeyword("_EMISSION");
                }  
                lightOn = false;
            }

            if(lightOn && (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) && robotScript.canPickupCone() && blue)
                {
                    robotScript.pickUpCone();
                    p.pickUpBlueCone();
                    Destroy(this.gameObject);
                }
            if (lightOn && (Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.O)) && robotScript2.canPickupCone() && !blue)
                {
                    robotScript2.pickUpCone();
                    p.pickUpRedCone();
                    Destroy(this.gameObject);

                }
        }
        }
        

        

    
    private void OnTriggerEnter(Collider c){
        
        if (c.gameObject.name.Contains("barrier"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            
            Collider col = GetComponent<Collider>();
            col.enabled = false;
            c.gameObject.transform.position = new Vector3(c.gameObject.transform.position.x, c.gameObject.transform.position.y + .375f, c.gameObject.transform.position.z);
        }
    }
}
