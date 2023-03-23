using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float minangle;
    public float maxangle;

    private void Start()
    {

    }

    public void Shot(Vector3 dir)
    {
       




        GetComponent<Rigidbody>().AddForce(dir);
        Vector3 vel = new Vector3(Random.Range(minangle, maxangle), Random.Range(minangle, maxangle), Random.Range(minangle, maxangle));
        GetComponent<Rigidbody>().velocity = vel;

    }
}
