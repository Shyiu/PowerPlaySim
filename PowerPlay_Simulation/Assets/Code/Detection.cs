using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    [SerializeField] GameObject obj;
    MeshRenderer meshRendererObj;
    Ray ray;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        meshRendererObj = obj.GetComponent<MeshRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && hit.collider.name == "Junction")
        {

            meshRendererObj.material.EnableKeyword("_EMISSION");

        }
        else
        {

            meshRendererObj.material.DisableKeyword("_EMISSION");
        }

    }

