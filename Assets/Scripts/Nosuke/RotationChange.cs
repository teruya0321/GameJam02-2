using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationChange : MonoBehaviour
{
    public Vector3 changeRotation;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localEulerAngles = changeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
