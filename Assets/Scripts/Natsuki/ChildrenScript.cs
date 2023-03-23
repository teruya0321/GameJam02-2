using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenScript : MonoBehaviour
{
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            //this.gameObject.transform.parent = prefab.gameObject.transform;
        }
    }
}
