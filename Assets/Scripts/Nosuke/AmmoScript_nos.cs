using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript_nos : MonoBehaviour
{
    public float speed;
    public int atk;
    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Player")
        {
            hit.gameObject.GetComponent<PlayerControl_Nat>().hp += atk;
            Destroy(gameObject);
        }
    }
}
