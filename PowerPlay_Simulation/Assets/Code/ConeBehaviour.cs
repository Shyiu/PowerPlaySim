using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float offset = 4f;
    private float ypos = 0;
    void Start()
    {
    
        ypos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Rigidbody>().mass < 600){
        transform.position = new Vector3(transform.position.x, ypos, transform.position.z);
        }

    }
    private void OnTriggerEnter(Collider c){
        
        if (GetComponent<Rigidbody>().velocity.y < 0)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            Collider col = GetComponent<Collider>();
            col.enabled = false;
            c.gameObject.transform.position = new Vector3(c.gameObject.transform.position.x, c.gameObject.transform.position.y + .375f, c.gameObject.transform.position.z);
            // Debug.Log("disabled cone colldier");
        }
    }
}
