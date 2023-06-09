using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject_nos : MonoBehaviour
{
    // Start is called before the first frame updatepdate is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if(pos.y <= -50)
        {
            Destroy(gameObject);
            //敵が地面をすり抜けてしまった時用に、一定の座標を下回ったら消すように
        }
    }
}
