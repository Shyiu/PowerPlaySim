using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierGeneration : MonoBehaviour
{
    public float offset = 4f;
    [SerializeField] Material transparentRed;
    [SerializeField] Material transparentBlue;
    private List<Collider> BlueRight;
    private List<Collider> BlueLeft;
    private List<Collider> RedRight;
    private List<Collider> RedLeft;
    private bool completed = false;
    GameObject barrier;
    
    private Dictionary<int,string> letter = new Dictionary<int,string>();
    // Start is called before the first frame update
    public List<Collider> getCones(string name)
    {
        if (name.Contains("Red"))
        {
            if (name.Contains("Right"))
            {
                return RedRight;
            }
            return RedLeft;
        }
        else
        {
            if (name.Contains("Right"))
            {
                return BlueRight;
            }
            return BlueLeft;
        }

    }
    void Start()
    {
        BlueRight = new List<Collider>();
        BlueLeft = new List<Collider>();
        RedRight = new List<Collider>();
        RedLeft = new List<Collider>();
        Application.targetFrameRate = 30;
        letter.Add(0,"A");
        letter.Add(1, "B");
        letter.Add(2, "C");
        letter.Add(3, "D");
        letter.Add(4, "E");
        Collider d = GameObject.Find("Barrier").gameObject.GetComponent<Collider>();
        Destroy(GameObject.Find("Barrier"));
        for(int r = 0; r < 5; r++){
            for(int c = 0; c < 5; c++){
                float x = (offset * c) - (2 * offset);
                float z = (offset * r) - (2 * offset);
                barrier = Instantiate(d.gameObject, new Vector3(x, 0.25f, z), Quaternion.identity);
                barrier.name = letter[r] + "" + (c+1) + "barrier";

            }
        }
        barrier = Instantiate(d.gameObject, new Vector3(11, 0.25f, -11), Quaternion.identity);
        barrier.name = "barrierRedBottomRight";
        barrier = Instantiate(d.gameObject, new Vector3(11, 0.25f, 11), Quaternion.identity);
        barrier.name = "barrierBlueTopRight";
        barrier = Instantiate(d.gameObject, new Vector3(-11, 0.25f, 11), Quaternion.identity);
        barrier.name = "barrierRedTopLeft";
        barrier = Instantiate(d.gameObject, new Vector3(-11, 0.25f, -11), Quaternion.identity);
        barrier.name = "barrierRedBottomLeft";
        GameObject blueCone = GameObject.Find("Blue_Cone_Sample").gameObject;
        for(int r = 0; r < 5; r++){
            GameObject blueConeClone = Instantiate(blueCone, new Vector3(11.4f, r * .25f, -2), Quaternion.identity);
            // Debug.Log(blueConeClone.transform.position.y);
             blueConeClone.gameObject.transform.localScale += new Vector3(9,9,9);
             blueConeClone.name = "Right Blue_Cone Stack " + r;
             Rigidbody blueConeRb =  blueConeClone.GetComponent<Rigidbody>();
             blueConeRb.useGravity = false;
             blueConeRb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ| RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationY;
             Collider blueConeCollider = blueConeClone.GetComponent<Collider>();
             BlueRight.Add(blueConeCollider);
        }
        for(int r = 0; r < 5; r++){
            GameObject blueConeClone = Instantiate(blueCone, new Vector3(-11.4f, r * .25f, -2), Quaternion.identity);
            blueConeClone.gameObject.transform.localScale += new Vector3(9,9,9);
             blueConeClone.name = "Left Blue_Cone Stack " + r;
             Rigidbody blueConeRb =  blueConeClone.GetComponent<Rigidbody>();
             blueConeRb.useGravity = false;
             blueConeRb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ| RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationY;
             Collider blueConeCollider = blueConeClone.GetComponent<Collider>();
             BlueLeft.Add(blueConeCollider);

        }
        int coneCount = 11;
        for(int c = 1; c <= 4; c++){
            if(c == 0){
                continue;
            }
            for(int r = 4; r >= 0; r--){
                GameObject blueConeClone = Instantiate(blueCone, new Vector3(1f * c - 2 * 1f, r * .25f, -12.5f), Quaternion.identity);
                blueConeClone.gameObject.transform.localScale += new Vector3(9,9,9);
                blueConeClone.name = "Blue Cone " + coneCount;
                Rigidbody blueConeRb =  blueConeClone.GetComponent<Rigidbody>();
                blueConeRb.useGravity = false;
                blueConeRb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ| RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationY;
                MeshRenderer blueConeCollider = blueConeClone.GetComponent<MeshRenderer>();
                blueConeCollider.material = transparentBlue;
                coneCount += 1;
            }
        }
        GameObject redCone = GameObject.Find("Red_Cone_Sample").gameObject;

        coneCount = 11;
        for(int c = 1; c <= 4; c++){
            if(c == 0){
                continue;
            }
            for(int r = 4; r >= 0; r--){
                GameObject blueConeClone = Instantiate(redCone, new Vector3(1f * c - 2 * 1f, r * .25f, 12.5f), Quaternion.identity);
                blueConeClone.gameObject.transform.localScale += new Vector3(9,9,9);
                blueConeClone.name = "Red Cone " + coneCount;
                Rigidbody blueConeRb =  blueConeClone.GetComponent<Rigidbody>();
                blueConeRb.useGravity = false;
                blueConeRb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ| RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationY;
                MeshRenderer blueConeCollider = blueConeClone.GetComponent<MeshRenderer>();
                blueConeCollider.material = transparentRed;
                coneCount += 1;
            }
        }
        for (int r = 0; r < 5; r++)
        {
            GameObject blueConeClone = Instantiate(redCone, new Vector3(11.4f, r * .25f, 2), Quaternion.identity);
            // Debug.Log(blueConeClone.transform.position.y);
            blueConeClone.gameObject.transform.localScale += new Vector3(9, 9, 9);
            blueConeClone.name = "Right Red_Cone Stack " + r;
            Rigidbody blueConeRb = blueConeClone.GetComponent<Rigidbody>();
            blueConeRb.useGravity = false;
            blueConeRb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            Collider blueConeCollider = blueConeClone.GetComponent<Collider>();
            RedRight.Add(blueConeCollider);
        }
        for (int r = 0; r < 5; r++)
        {
            GameObject blueConeClone = Instantiate(redCone, new Vector3(-11.4f, r * .25f, 2), Quaternion.identity);
            blueConeClone.gameObject.transform.localScale += new Vector3(9, 9, 9);
            blueConeClone.name = "Left Red_Cone Stack " + r;
            Rigidbody blueConeRb = blueConeClone.GetComponent<Rigidbody>();
            blueConeRb.useGravity = false;
            blueConeRb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            Collider blueConeCollider = blueConeClone.GetComponent<Collider>();
            RedLeft.Add( blueConeCollider);

        }
        completed = true;

    }
    public bool ready()
    {
        return completed;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
