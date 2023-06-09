using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationChange : MonoBehaviour
{
    // モデルをUnityに持ってきた際に回転の値がおかしいことがあったのでUnity上で変更するためのスクリプト
    // 特に使うことはなかったけど

    public Vector3 changeRotation;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localEulerAngles = changeRotation;
        // 入力された数字を基に角度を変更
    }
}
