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
    // Start is called before the first frame update
    void Start()
    {

        obj = Robot;
        meshRendererObj = obj.GetComponent<MeshRenderer>();
        priorObj = Robot;
        conePickedUp = false;
        
    }


    // Update is called once per frame
    bool Scan(){
        hostPos = Robot.transform.position;
        direction = Robot.transform.forward;
        ray = new Ray(hostPos, direction);
        
        if (Physics.Raycast(ray, out hit, distance) )
        {
            if(hit.collider.gameObject.name.Contains("Wall") || hit.collider.gameObject.name.Contains("Cone")){
                return false;
            }
            if(obj == null){
                return false;
            }
            if(!obj.name.Equals(priorObj.name)){
                meshRendererObj = priorObj.GetComponent<MeshRenderer>();
                for (int i = 0; i < meshRendererObj.materials.Length; i++)
                {
                    meshRendererObj.materials[i].DisableKeyword("_EMISSION");
                }
            }
            obj = hit.collider.gameObject;
            priorObj = obj;
            meshRendererObj = obj.GetComponent<MeshRenderer>();
            for (int i = 0; i < meshRendererObj.materials.Length;i++)
            {
                meshRendererObj.materials[i].EnableKeyword("_EMISSION");
            }
            return true;
         }
        else
        { 
                if(obj == null){
                    return false;
                }
                for (int i = 0; i < meshRendererObj.materials.Length; i++)
                {
                    meshRendererObj.materials[i].DisableKeyword("_EMISSION");
                }
                return false;
        }
        
    }
    void coneHit(){
        hostPos = Robot.transform.position;
        direction = Robot.transform.forward;
        ray = new Ray(hostPos, direction);
        if (Physics.Raycast(ray, out hit, coneDistance) )
        {
            obj = hit.collider.gameObject;
            if(obj != null){
            priorCone = obj;
            meshRendererObj = obj.GetComponent<MeshRenderer>();
            if( hit.collider.gameObject.name.Contains("Cone")){     
                for (int i = 0; i < meshRendererObj.materials.Length;i++)
                {
                meshRendererObj.materials[i].EnableKeyword("_EMISSION");
            }   
                if(Input.GetKeyDown(KeyCode.Alpha2) && !conePickedUp){         
                    conePickedUp = true;
                    Destroy(hit.collider.gameObject);
                    return;
                }
                
            }
            }
            else{
                return;
            }
        }
        
         else{
            obj = priorCone;
            if(obj != null){
                meshRendererObj = obj.GetComponent<MeshRenderer>();
                if( hit.collider.gameObject.name.Contains("Cone")){     
                    for (int i = 0; i < meshRendererObj.materials.Length;i++)
                    {
                    meshRendererObj.materials[i].EnableKeyword("_EMISSION");
                }   
            }else{
                return;
            }
         }
         }
        

    }
    void Update()
    {
       if(Scan()){
            if(Input.GetKeyDown(KeyCode.Alpha1) && conePickedUp){  
                 conePickedUp = false;       
                 GameObject newBlueCone = Instantiate(blueCone, new Vector3(obj.gameObject.transform.position.x,obj.gameObject.transform.position.y + 10,obj.gameObject.transform.position.z), Quaternion.identity);
                 newBlueCone.gameObject.transform.localScale += new Vector3(9,9,9);
                 Rigidbody blueConeRb =  newBlueCone.GetComponent<Rigidbody>();
                 blueConeRb.mass = 625;
                 blueConeRb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ| RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationY;
            }
       }  
       coneHit();
        
       
    }

}