using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeStackBehaviour : MonoBehaviour
{
    RaycastHit[] r;
    List<GameObject> cones = new List<GameObject>();
    bool full = false;
    Collider c;
    void Start()
    {
        c = GameObject.Find("ConeStackCollider").GetComponent<Collider>();
        

    }
  
    // Update is called once per frame
    void Update()
    {
        if (!full)
        {
            r = Physics.BoxCastAll(new Vector3(11.5f, 0f, -0.65f), new Vector3(0.625f, 1.25f, 0.625f), new Vector3(0, 1, 0));
            Debug.Log("Wait");
            foreach (RaycastHit x in r)
            {
                if (x.collider.gameObject.name.Contains("Blue_Cone"))
                {
                    cones.Add(x.collider.gameObject);
                }
            }
            if(cones.Count == 5)
            {
                full = true;
            }
        }
        Debug.Log(cones.Count);
    }
}
