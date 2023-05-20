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
    RaycastHit hit;
    // private int coneId;
    [SerializeField] GameObject Robot;
    [SerializeField] GameObject blueCone;
    public float distance = 2.5f;
    // Start is called before the first frame update
    void Start()
    {

        obj = Robot;
        meshRendererObj = obj.GetComponent<MeshRenderer>();
        priorObj = Robot;
        
    }


    // Update is called once per frame
    bool Scan(){
        hostPos = Robot.transform.position;
        direction = Robot.transform.forward;
        ray = new Ray(hostPos, direction);
        
        if (Physics.Raycast(ray, out hit, distance) )
        {
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
                for (int i = 0; i < meshRendererObj.materials.Length; i++)
                {
                    meshRendererObj.materials[i].DisableKeyword("_EMISSION");
                }
                return false;
        }
        
    }
    void Update()
    {
       if(Scan()){
            if(Input.GetKeyDown(KeyCode.Alpha1)){         
                 GameObject newBlueCone = Instantiate(blueCone, new Vector3(obj.gameObject.transform.position.x,obj.gameObject.transform.position.y + 10,obj.gameObject.transform.position.z), Quaternion.identity);
                 newBlueCone.gameObject.transform.localScale += new Vector3(10,10,10);
                 Rigidbody blueConeRb =  newBlueCone.GetComponent<Rigidbody>();
                 blueConeRb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ| RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationY;
            }
       }  
    }

}