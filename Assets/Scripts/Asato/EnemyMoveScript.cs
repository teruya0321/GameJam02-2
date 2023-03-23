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
        //enemy�̍s�������@Player��������
        enemTimer += Time.deltaTime;
        if (enemTimer > enemTimerRimit + enemTimerRandom && !isLookPlayer)
        {
            enemActions();
        }

        //�ړ���������
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
                actWord="�O�i";
                moveDirection.z = enemSpeedZ * Time.deltaTime;
                float moveWait0 = 3;
                if (enemTimer > moveWait0) enemActionReset();
                break;
            case 1:
                actWord = "�E����";
                transform.Rotate(0, enemSpeedRot * Time.deltaTime, 0);
                float moveWait1 = 2f;
                if (enemTimer > moveWait1) enemActionReset();
                break;
            case 2:
                actWord = "������";
                transform.Rotate(0, enemSpeedRot * Time.deltaTime*-1, 0);
                float moveWait2 = 2f;
                if (enemTimer > moveWait2) enemActionReset();
                break;
            case 3:
                actWord = "�U��Ԃ�";
                transform.Rotate(0, enemSpeedRot * Time.deltaTime*2, 0);
                float moveWait3 = 2.5f;
                if (enemTimer > moveWait3) enemActionReset();
                break;
            case 4:
                actWord = "�_�b�V��";
                moveDirection.z = enemSpeedZ * Time.deltaTime*3;
                float moveWait4 = 1;
                if (enemTimer > moveWait4) enemActionReset();
                break;
            case 5:
                actWord = "�ҋ@";
                moveDirection.z *= 0.6f;
                float moveWait5 = 5;
                if (enemTimer > moveWait5) enemActionReset();
                break;
            case 6:
                actWord = "�W�����v";
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
        enemTimer = 0;//�^�C�}�[���Z�b�g
    }

}
