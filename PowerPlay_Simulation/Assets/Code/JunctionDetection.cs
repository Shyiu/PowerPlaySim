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
    private float distance = 2.5f;
    private float distanceOffset = 4;
    Detection d;
    Detection d2;
    ScoreBoard s;
    private float row = 0;
    private float col = 0;
    private float heightConstant = 3.5f;
    public float coneLimit = 8;
    private bool mouseDetection = false;
    private string junctionType = "";
    private int conesPlaced = 0;
    private bool clickTrigger = false;
    private Dictionary<string,float> heights = new Dictionary<string,float>();
    private Dictionary<string, int> conversion = new Dictionary<string, int>();
    
    // Start is called before the first frame update
    void Start()
    {
        heights.Add("Short", 5f);
        heights.Add("Ground", 1f);
        heights.Add("Medium", 7f);
        heights.Add("High", 10f);
        robot = GameObject.Find("Robot1");
        blueCone = GameObject.Find("Blue_Cone_Sample");
        robot2 = GameObject.Find("Robot2");
        redCone = GameObject.Find("Red_Cone_Sample");
        conversion.Add("Ground", 0);
        conversion.Add("Short", 1);
        conversion.Add("Medium", 2);
        conversion.Add("High", 3);
        meshRendererObj = GetComponent<MeshRenderer>();
        d = robot.GetComponent<Detection>();
        s = GameObject.Find("Canvas").GetComponent<ScoreBoard>();
        d2 = robot2.GetComponent<Detection>();
        row = (gameObject.transform.position.z)/distanceOffset;
        col = (gameObject.transform.position.x)/distanceOffset;
        if (gameObject.name.Contains("Ground"))
        {
            ;
        }
        else
        {
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * -1f, ForceMode.Impulse);
        }
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        foreach(KeyValuePair<string, float> kvp in heights)
            {
                if(gameObject.name.Contains(kvp.Key)){
                    heightConstant = kvp.Value;
                    junctionType = kvp.Key;
                }
            }
    }
    public void enableMouse(){
        mouseDetection = true;
    }
    public void disableMouse(){
        mouseDetection = false;
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
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
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
        else if(mouseDetection && Physics.Raycast(mouseRay, out hit) && hit.collider.name.Equals(gameObject.name) && conesPlaced < coneLimit){
            for (int i = 0; i < meshRendererObj.materials.Length; i++)
            {
                meshRendererObj.materials[i].EnableKeyword("_EMISSION");
            }
            clickTrigger = true;
        }
        else
        {
            for (int i = 0; i < meshRendererObj.materials.Length; i++)
                {
                    meshRendererObj.materials[i].DisableKeyword("_EMISSION");
                }
                emissionToggle = false;
                emissionToggle2 = false;
                clickTrigger = false;
        }
        
        
        if (emissionToggle){
            if((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) && !d.canPickupCone() && conesPlaced < coneLimit){
                d.scoreCone();
                conesPlaced += 1;
                GameObject newBlueCone = Instantiate(blueCone, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + heightConstant, gameObject.transform.position.z), Quaternion.identity);
                newBlueCone.gameObject.transform.localScale += new Vector3(9,9,9);
                Rigidbody blueConeRb =  newBlueCone.GetComponent<Rigidbody>();
                blueConeRb.mass = 625;
                newBlueCone.gameObject.name = gameObject.name + "Cone" + conesPlaced;
                blueConeRb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ| RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationY;
                s.placeBlueCone(conversion[junctionType], row, col);
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
                newRedCone.gameObject.name = gameObject.name + "Cone" + conesPlaced;
                Rigidbody RedConeRb = newRedCone.GetComponent<Rigidbody>();
                RedConeRb.mass = 625;
                RedConeRb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                s.placeRedCone(conversion[junctionType], row, col);
            }
        }
        if(clickTrigger && Input.GetMouseButtonDown(0)){
            d.scoreCone();
                conesPlaced += 1;
                GameObject newBlueCone = Instantiate(blueCone, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + heightConstant, gameObject.transform.position.z), Quaternion.identity);
                newBlueCone.gameObject.transform.localScale += new Vector3(9,9,9);
                Rigidbody blueConeRb =  newBlueCone.GetComponent<Rigidbody>();
                blueConeRb.mass = 625;
                blueConeRb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ| RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationY;
                s.placeBlueCone(conversion[junctionType], row, col);
        }
        if(clickTrigger && Input.GetMouseButtonDown(1)){
                d2.scoreCone();
                conesPlaced += 1;
                GameObject newRedCone = Instantiate(redCone, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + heightConstant, gameObject.transform.position.z), Quaternion.identity);
                newRedCone.gameObject.transform.localScale += new Vector3(9, 9, 9);
                Rigidbody RedConeRb = newRedCone.GetComponent<Rigidbody>();
                RedConeRb.mass = 625;
                RedConeRb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                s.placeRedCone(conversion[junctionType], row, col);
        }
    }
}
