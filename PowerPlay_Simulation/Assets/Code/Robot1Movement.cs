using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot1Movement : MonoBehaviour
{
    public float speed = 10;
    public float turnspeed = 10;
    private Rigidbody rb;
    private bool stopped = false;
    private float delay = 1f;

    // Start is called before the first frame update

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * -1 * speed, ForceMode.Impulse);
        rb.AddRelativeForce(Vector3.up * 5, ForceMode.Impulse);

        rb.constraints =  RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y == 0 && Time.realtimeSinceStartup > delay)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            stopped = true;
        }


        if (stopped){
            float h = Input.GetAxisRaw("Horizontal");   
            float v = Input.GetAxisRaw("Vertical");
    
            rb.AddRelativeForce(Vector3.forward * v * speed, ForceMode.Impulse);
            gameObject.transform.Rotate(0.0f, turnspeed * h, 0.0f, Space.Self);

        }
    }
}
