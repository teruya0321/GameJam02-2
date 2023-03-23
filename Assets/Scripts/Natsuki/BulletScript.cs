using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float minangle;
    public float maxangle;

    PlayerControl_Nat playerControl_Nat;
    

    void Start()
    {
        int Limit;
        Limit = playerControl_Nat.Getlimit();
        playerControl_Nat = GetComponent<PlayerControl_Nat>();
    }

    void Update()
    {
        
    }

    public void Shot(Vector3 dir)
    {
        int Limit;
        Limit = playerControl_Nat.Getlimit();

        if (Limit == 1000)
        {
            maxangle = -10;
            minangle = 10;
            Debug.Log(maxangle);
            Debug.Log(minangle);

        }

        GetComponent<Rigidbody>().AddForce(dir);
        Vector3 vel = new Vector3(Random.Range(minangle, maxangle), Random.Range(minangle, maxangle), Random.Range(minangle, maxangle));
        GetComponent<Rigidbody>().velocity = vel;
    }
}
