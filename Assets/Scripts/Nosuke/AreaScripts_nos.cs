using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaScripts_nos : MonoBehaviour
{
    // �G���A�g��p�̃X�N���v�g
    Vector3 areaSize = new Vector3(1, 25, 1);
    private void Update()
    {
        areaSize.x += Time.deltaTime / 50;
        areaSize.z += Time.deltaTime / 50;

        gameObject.transform.localScale = areaSize;
        // Object�̃T�C�Y��Vector3�ɑ���AVector3��x��z�����Ԍo�߂ő��������ăG���A���L���Ă���
    }
}
