using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider c){
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ; 
        Collider col = GetComponent<Collider>();
        col.enabled = false;
        GameObject barrier = Instantiate(c.gameObject, new Vector3(c.gameObject.transform.position.x, c.gameObject.transform.position.y + .235f, c.gameObject.transform.position.z), Quaternion.identity);

    }
}
