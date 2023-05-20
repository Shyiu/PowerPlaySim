using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot1Movement : MonoBehaviour
{
    public float speed = 10;
    public float turnspeed = 10;
    private Rigidbody rb;
    private bool stopped = false;
    // Start is called before the first frame update
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        float h = Input.GetAxisRaw("Horizontal");   
        float v = Input.GetAxisRaw("Vertical");
        rb.AddRelativeForce(Vector3.forward * v * speed, ForceMode.Impulse);
        rb.AddTorque(Vector3.up * h * turnspeed, ForceMode.Impulse);
        }
    }
}
