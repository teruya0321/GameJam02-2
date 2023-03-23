using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveScript : MonoBehaviour
{
    CharacterController ChaCon;
    Vector3 moveDirection = Vector3.zero;

    public float enemGravity;
    public float enemSpeedZ;
    public float enemSpeedRot;
    public float enemJump;

    float enemTimer = 0;
    int enemAction=5;
    public float enemTimerRimit;
    public float enemTimerRandom;

    bool isLookPlayer = false;

    void Start()
    {
        Application.targetFrameRate = 60;
        ChaCon = GetComponent<CharacterController>();
    }

    void Update()
    {
        //enemyの行動周期　Player未発見時
        enemTimer += Time.deltaTime;
        if (enemTimer > enemTimerRimit + enemTimerRandom && !isLookPlayer)
        {
            enemActions();
        }

        //移動落下処理
        moveDirection.y -= enemGravity * Time.deltaTime;
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        ChaCon.Move(globalDirection * Time.deltaTime);

        if (ChaCon.isGrounded)
        {
            moveDirection.y = 0;



        }
    }

    void enemActions()
    {
        string actWord;

        switch (enemAction)
        {
            case 0:
                actWord="前進";
                moveDirection.z = enemSpeedZ * Time.deltaTime;
                float moveWait0 = 3;
                if (enemTimer > moveWait0) enemActionReset();
                break;
            case 1:
                actWord = "右旋回";
                transform.Rotate(0, enemSpeedRot * Time.deltaTime, 0);
                float moveWait1 = 2f;
                if (enemTimer > moveWait1) enemActionReset();
                break;
            case 2:
                actWord = "左旋回";
                transform.Rotate(0, enemSpeedRot * Time.deltaTime*-1, 0);
                float moveWait2 = 2f;
                if (enemTimer > moveWait2) enemActionReset();
                break;
            case 3:
                actWord = "振り返り";
                transform.Rotate(0, enemSpeedRot * Time.deltaTime*2, 0);
                float moveWait3 = 2.5f;
                if (enemTimer > moveWait3) enemActionReset();
                break;
            case 4:
                actWord = "ダッシュ";
                moveDirection.z = enemSpeedZ * Time.deltaTime*3;
                float moveWait4 = 1;
                if (enemTimer > moveWait4) enemActionReset();
                break;
            case 5:
                actWord = "待機";
                moveDirection.z *= 0.6f;
                float moveWait5 = 5;
                if (enemTimer > moveWait5) enemActionReset();
                break;
            case 6:
                actWord = "ジャンプ";
                moveDirection.y = enemJump * Time.deltaTime;
                enemActionReset();
                break;

            default:
                actWord = "Default";
                break;
        }
        Debug.Log(actWord);





    }
    void enemActionReset()
    {
        enemAction = Random.Range(0, 7);
        enemTimer = 0;//タイマーリセット
    }

}
