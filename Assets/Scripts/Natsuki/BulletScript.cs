using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float minangle;
    public float maxangle;

    PlayerControl_Nat playerControl_Nat;

    private void Start()
    {
        playerControl_Nat = GetComponent<PlayerControl_Nat>(); //付いているスクリプトを取得
    }

    public void Shot(Vector3 dir)
    {
       // if (limit == 0)




        GetComponent<Rigidbody>().AddForce(dir);
        Vector3 vel = new Vector3(Random.Range(minangle, maxangle), Random.Range(minangle, maxangle), Random.Range(minangle, maxangle));
        GetComponent<Rigidbody>().velocity = vel;

    }
}
