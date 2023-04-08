using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove_nos : MonoBehaviour
{
    GameObject player;
    // �v���C���[�I�u�W�F�N�g
    public float distance = 10;
    // �����𑪂鐔�l
    CharacterController ChaCon;
    // ���������߂̃R���C�_�[
    Vector3 moveDirection = Vector3.zero;

    public float enemGravity;
    public float enemSpeedZ;
    public float enemSpeedRot;
    public float enemJump;
    // ���ꂼ��̓G�̐��l
    float enemTimer = 0;
    int enemAction = 5;

    public float enemTimerRimit;
    public float enemTimerRandom;

    bool isLookPlayer = false;

    EnemyModel_nos model;

    float raytimer;

    float timer;

    void Start()
    {
        Application.targetFrameRate = 60;
        // �����Ă�������˂���
        ChaCon = GetComponent<CharacterController>();
        model = GetComponent<EnemyModel_nos>();

        player = GameObject.Find("MainCharaAnim");
        // �v���C���[��ݒ�
        if(model.speed == 1)
        {
            enemSpeedZ /= 2;
            
        }
        else if(model.speed == 3)
        {
            enemSpeedZ *= 2;
        }
        // �L�����ɂ���ăX�s�[�h���O�i�K�ɕ�����
    }
    private void Update()
    {
        // Raycast�̔���
        Ray rayPosition = new Ray(transform.position, transform.forward.normalized * distance);
        RaycastHit raycastHit;
        Debug.DrawRay(transform.position, transform.forward.normalized * distance, Color.red);
        if (Physics.Raycast(rayPosition, out raycastHit, distance))
        {
            float posdistance = Vector3.Distance(transform.position,raycastHit.transform.position);
            
            //Debug.Log("����������");
            if (raycastHit.collider.gameObject.tag == "Player")
            {
                if(posdistance <= 2 && model.skinny == true)
                {
                    timer += Time.deltaTime;
                    if(timer >= 1)
                    {
                        raycastHit.collider.gameObject.GetComponent<PlayerControl_Nat>().hp += model.enemyAtk;
                        timer = 0;
                    }
                }
                else
                {
                    timer = 0;
                }
                //isLookPlayer = true;
                // �������Ă���Ԃ͒ǂ�������ݒ��true��
                raytimer = 0;
                Debug.Log("����������");
            }
            else
            {
                //isLookPlayer = false;
                // �������Ă��Ȃ����false�ɂ���
                raytimer += Time.deltaTime;
                //Debug.Log("�������ĂȂ���");
            }
            
        }

        if(raytimer <= 2)
        {
            isLookPlayer = true;
        }
        else
        {
            isLookPlayer = false;
        }
        enemTimer += Time.deltaTime;
        // �G�̃����_���s���p�̃^�C�}�[
        if (enemTimer > enemTimerRimit + enemTimerRandom && !isLookPlayer)
        {
            // �^�C�}�[����莞�Ԍo���āA�Ȃ����v���C���[�������Ă��Ȃ�������A���̍s���Ɉڂ�
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
        // �v���C���[�������Ă�����
        if (isLookPlayer)
        {
            FoundPlayer();
            // �G���������U���ł���L�����Ȃ�
            if (model.skinny == false)
            {
                model.LongAttack();
            }
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

    

    // �G�̃����_���s�������߂�֐�
    void enemActions()
    {
        //string actWord;
        // �s���������_���Ɍ��߂�
        switch (enemAction)
        {
            case 0:
                //actWord = "�O�i";
                moveDirection.z = enemSpeedZ * Time.deltaTime;
                float moveWait0 = 3;
                if (enemTimer > moveWait0) enemActionReset();
                break;
            case 1:
                //actWord = "�E����";
                transform.Rotate(0, enemSpeedRot * Time.deltaTime, 0);
                float moveWait1 = 2f;
                if (enemTimer > moveWait1) enemActionReset();
                break;
            case 2:
                //actWord = "������";
                transform.Rotate(0, enemSpeedRot * Time.deltaTime * -1, 0);
                float moveWait2 = 2f;
                if (enemTimer > moveWait2) enemActionReset();
                break;
            case 3:
                //actWord = "�U��Ԃ�";
                transform.Rotate(0, enemSpeedRot * Time.deltaTime * 2, 0);
                float moveWait3 = 2.5f;
                if (enemTimer > moveWait3) enemActionReset();
                break;
            case 4:
                //actWord = "�_�b�V��";
                moveDirection.z = enemSpeedZ * Time.deltaTime * 3;
                float moveWait4 = 1;
                if (enemTimer > moveWait4) enemActionReset();
                break;
            case 5:
                //actWord = "�ҋ@";
                moveDirection.z *= 0.6f;
                float moveWait5 = 5;
                if (enemTimer > moveWait5) enemActionReset();
                break;
            case 6:
                //actWord = "�W�����v";
                moveDirection.y = enemJump * Time.deltaTime;
                enemActionReset();
                break;

            default:
                //actWord = "Default";
                break;
        }
        //Debug.Log(actWord);
    }

    // �s�������߂����Ƃ̃^�C�}�[�̃��Z�b�g�Ǝ��̍s���p�^�[�������߂�֐�
    void enemActionReset()
    {
        enemAction = Random.Range(0, 7);
        enemTimer = 0;//�^�C�}�[���Z�b�g
    }
}
