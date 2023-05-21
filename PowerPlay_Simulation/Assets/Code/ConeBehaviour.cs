using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float[,] yvalues;
    public float offset = 4f;
    void Start()
    {
        
        yvalues = new float[5,5];
        for(int r = 0; r < 5; r++){
            for(int c = 0; c < 5; c++){
                yvalues[r,c] = 0;
                

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider c){
        float x = gameObject.transform.position.x;
        float z = gameObject.transform.position.z;
        Debug.Log("x: " + x);
        Debug.Log("z: " + z);
        x += 2 * offset;
        z += 2 * offset;
        x /= offset;
        z /= offset;
        yvalues[(int) z, (int) x] += .25f;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ; 
        Collider col = GetComponent<Collider>();
        col.enabled = false;
        GameObject barrier = Instantiate(c.gameObject, new Vector3(gameObject.transform.position.x, yvalues[((int) z),((int) x)], gameObject.transform.position.z), Quaternion.identity);
    }
}
