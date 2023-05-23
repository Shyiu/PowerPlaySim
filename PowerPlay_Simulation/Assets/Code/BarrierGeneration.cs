using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierGeneration : MonoBehaviour
{
    public float offset = 4f;
    // Start is called before the first frame update
    void Start()
    {
        Collider d = GameObject.Find("Barrier").gameObject.GetComponent<Collider>();
        Destroy(GameObject.Find("Barrier"));
        for(int r = 0; r < 5; r++){
            for(int c = 0; c < 5; c++){
                float x = (offset * c) - (2 * offset);
                float z = (offset * r) - (2 * offset);
                
                GameObject barrier = Instantiate(d.gameObject, new Vector3(x, 0, z), Quaternion.identity);

            }
        }
        GameObject blueCone = GameObject.Find("Blue_Cone").gameObject;
        for(int r = 0; r < 5; r++){
            GameObject blueConeClone = Instantiate(blueCone, new Vector3(11.5f, r * .25f, -2 * 0.4375f), Quaternion.identity);
             blueConeClone.gameObject.transform.localScale += new Vector3(9,9,9);
             Rigidbody blueConeRb =  blueConeClone.GetComponent<Rigidbody>();
             blueConeRb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ| RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationY;
             Collider blueConeCollider = blueConeClone.GetComponent<Collider>();
        }
        for(int r = 0; r < 5; r++){
            GameObject blueConeClone = Instantiate(blueCone, new Vector3(-11.5f, r * .25f, -2 * 0.4375f), Quaternion.identity);
             blueConeClone.gameObject.transform.localScale += new Vector3(9,9,9);
             Rigidbody blueConeRb =  blueConeClone.GetComponent<Rigidbody>();
             blueConeRb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ| RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationY;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
