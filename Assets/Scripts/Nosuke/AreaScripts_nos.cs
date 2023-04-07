using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaScripts_nos : MonoBehaviour
{
    Vector3 areaSize = new Vector3(1, 25, 1);
    private void Update()
    {
        areaSize.x += Time.deltaTime / 10;
        areaSize.z += Time.deltaTime / 10;

        gameObject.transform.localScale = areaSize;
    }
}
