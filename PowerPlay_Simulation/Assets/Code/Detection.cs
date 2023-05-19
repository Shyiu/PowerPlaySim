using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    [SerializeField] GameObject obj;
    MeshRenderer meshRendererObj;
    Vector3 hostPos;
    Vector3 targetPos;
    Ray ray;
    RaycastHit hit;
    public float distance = 10f;
    // Start is called before the first frame update
    void Start()
    {
        hostPos = GameObject.Find("Robot1").transform.position;
        meshRendererObj = obj.GetComponent<MeshRenderer>();
        targetPos = obj.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        ray = new Ray(hostPos, (targetPos - hostPos).normalized * 10);
        Debug.Log(hostPos);
        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.name);
        }

        if (Physics.Raycast(ray, out hit, distance) && hit.collider.gameObject == obj)
        {
            Debug.Log("contact");
            
            for (int i = 0; i < meshRendererObj.materials.Length;i++)
            {
                meshRendererObj.materials[i].EnableKeyword("_EMISSION");
            }
           

        }
        else
        {

            for (int i = 0; i < meshRendererObj.materials.Length; i++)
            {
                meshRendererObj.materials[i].DisableKeyword("_EMISSION");
            }
        }

    }

}