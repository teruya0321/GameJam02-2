using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove_nos : MonoBehaviour
{
    public GameObject player;
    public float distance = 10;

    CharacterController ChaCon;
    Vector3 moveDirection = Vector3.zero;

    public float enemGravity;
    public float enemSpeedZ;
    public float enemSpeedRot;
    public float enemJump;

    float enemTimer = 0;
    int enemAction = 5;
    public float enemTimerRimit;
    public float enemTimerRandom;

    bool isLookPlayer = false;

    EnemyModel_nos model;

    

    void Start()
    {
        Application.targetFrameRate = 60;
        ChaCon = GetComponent<CharacterController>();
        model = GetComponent<EnemyModel_nos>();
        if(model.speed == 1)
        {
            enemSpeedZ /= 2;
        }
        else if(model.speed == 3)
        {
            enemSpeedZ *= 2;
        }
    }
    private void Update()
    {
        Ray rayPosition = new Ray(transform.position, transform.forward.normalized * distance);
        RaycastHit raycastHit;
        Debug.DrawRay(transform.position, transform.forward.normalized * distance, Color.red);
        if (Physics.Raycast(rayPosition, out raycastHit, distance))
        {
            //Debug.Log("����������");
            if (raycastHit.collider.CompareTag("Player"))
            {
                isLookPlayer = true;
                Debug.Log("����������");
            }
            else
            {
                isLookPlayer = false;
                Debug.Log("�������ĂȂ���");
            }
        }
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
        if (isLookPlayer)
        {
            FoundPlayer();
        }
    }

    void FoundPlayer()
    {
        // �Ώە��Ǝ������g�̍��W����x�N�g�����Z�o
        Vector3 vector3 = player.transform.position - this.transform.position;
        // �����㉺�����̉�]�͂��Ȃ��悤�ɂ�������Έȉ��̂悤�ɂ���B
        // vector3.y = 0f;

        // Quaternion(��]�l)���擾
        Quaternion quaternion = Quaternion.LookRotation(vector3);
        // �Z�o������]�l�����̃Q�[���I�u�W�F�N�g��rotation�ɑ��
        this.transform.rotation = quaternion;

        moveDirection.z = enemSpeedZ * Time.deltaTime * 4;
    }

    void enemActions()
    {
        string actWord;

        switch (enemAction)
        {
            case 0:
                actWord = "�O�i";
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
                transform.Rotate(0, enemSpeedRot * Time.deltaTime * -1, 0);
                float moveWait2 = 2f;
                if (enemTimer > moveWait2) enemActionReset();
                break;
            case 3:
                actWord = "�U��Ԃ�";
                transform.Rotate(0, enemSpeedRot * Time.deltaTime * 2, 0);
                float moveWait3 = 2.5f;
                if (enemTimer > moveWait3) enemActionReset();
                break;
            case 4:
                actWord = "�_�b�V��";
                moveDirection.z = enemSpeedZ * Time.deltaTime * 3;
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
    /* //�v���C���[
    public GameObject player;
    private NavMeshAgent navMeshAgent;
    //�����_���ɕ�����ς��Ĉړ�
    private float chargeTime;
    private float timeCount;
    //ray�̋���
    public float distance;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        chargeTime = 3;
    }
    void Update()
    {
        Ray rayPosition = new Ray(transform.position, transform.forward.normalized * distance);
        RaycastHit raycasthit;
        Debug.DrawRay(transform.position, transform.forward.normalized * distance, Color.red);
        if (Physics.Raycast(rayPosition, out raycasthit, distance))
        {
            if (raycasthit.collider.CompareTag("Player"))
            {
                Debug.Log("�v���C���[�ɓ�������");
                navMeshAgent.destination = player.transform.position;
            }
        }
        else
        {
            Debug.Log("�T����");
            //�����]���̎��Ԍo��
            timeCount += Time.deltaTime;
            //�O�i�݂܂�
            transform.position += transform.forward * Time.deltaTime;
        }
        //���̎��Ԍo�߂ŕ����]��
        if (timeCount > chargeTime)
        {
            //�����_���Ɋp�x�ύX
            Vector3 course = new Vector3(0, UnityEngine.Random.Range(0, 360), 0);
            transform.localRotation = Quaternion.Euler(course);
            //0�ɖ߂�
            timeCount = 0;
        }
    }*/


}
