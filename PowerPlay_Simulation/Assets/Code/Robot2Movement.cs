using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot2Movement : MonoBehaviour
{
    public float speed = 10;
    public float turnspeed = 10;
    private Rigidbody rb;
    private bool stopped = false;
    // Start is called before the first frame update
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * -1 * speed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y == 0)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            
            stopped = true;
        }
     
        if(stopped){
            float h = Input.GetAxisRaw("Horizontal2");   
            float v = Input.GetAxisRaw("Vertical2");
           // rb.AddForceAtPosition(new Vector3(0, v * speed, 0), new Vector3() )
            rb.AddRelativeForce(Vector3.forward * v * speed, ForceMode.Impulse);
           
           
           gameObject.transform.Rotate(0.0f, turnspeed * h, 0.0f, Space.Self);


           
        }
    }
}
