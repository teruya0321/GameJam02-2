using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript_nos : MonoBehaviour
{
    public float speed;
    public int atk;
    float timer;
    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            Destroy();
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Player")
        {
            hit.gameObject.GetComponent<PlayerControl_Nat>().hp += atk;
            Destroy();
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 5)
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
