using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaScripts_nos : MonoBehaviour
{
    // エリア拡大用のスクリプト
    Vector3 areaSize = new Vector3(1, 25, 1);
    private void Update()
    {
        areaSize.x += Time.deltaTime / 50;
        areaSize.z += Time.deltaTime / 50;

        gameObject.transform.localScale = areaSize;
        // ObjectのサイズをVector3に代入、Vector3のxとzを時間経過で増加させてエリアを広げている
    }
}
