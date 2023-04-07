using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinFannelBeamScript : MonoBehaviour
{
    public float minangle;
    public float maxangle;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void Shot(Vector3 dir)
    {
        GetComponent<Rigidbody>().AddForce(dir);
        Vector3 vel = new Vector3(Random.Range(minangle, maxangle), Random.Range(minangle, maxangle), Random.Range(minangle, maxangle));
        GetComponent<Rigidbody>().velocity = vel;
    }
}
