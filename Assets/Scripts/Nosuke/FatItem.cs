using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatItem : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControl_Nat>().hp += 10;
            Destroy(gameObject);
        }
    }
}
