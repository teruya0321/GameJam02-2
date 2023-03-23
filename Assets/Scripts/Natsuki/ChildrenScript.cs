using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenScript : MonoBehaviour
{
    bool ishave = true;
    float timer = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!ishave)
        {
            timer -= Time.deltaTime;
            GetComponent<BoxCollider>().enabled = false;
        }

        if(timer <= 0)
        {
            ishave = true;
            timer = 3;
            GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void remove()
    {
        transform.SetParent(null);
        ishave = false;
    }
}
