using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject_nos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 pos = transform.position;
        if(pos.x <= -50)
        {
            Destroy(gameObject);
        }
    }
}
