using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript_nos : MonoBehaviour
{
    public int atk;
    // �U���� ���������ۂɕʂ̃X�N���v�g�����������
    float timer;
    // ���Ԍo�߂ŏ������߂̃^�C�}�[

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Ground")
        {
            Destroy();
            // �ǂ��n�ʂɒ��e����������悤��
        }
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControl_Nat>().hp += atk * 2;
            Destroy();
            // �v���C���[�ɒ��e������A�v���C���[�̑̏d���U���͕����₵�ď���
            // �Q�[���o�����X�I�ɍU���͂��{�ɁB�K�v�Ȃ����*2�������Ă��悢
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Player")
        {
            hit.gameObject.GetComponent<PlayerControl_Nat>().hp += atk * 2;
            Destroy();
            // ���OncollisionEnter�ŏ��������e�Ɠ����ł�
            // �������e���x�����Ă闝�R�́A�v���C���[�̃��f����Collider��CharacterControler�̓��ނ�Component�����Ă���A�ǂ����Ŕ������Ă���̂��m�F���鎞�Ԃ����������̂ŁA
            // ����̍�łǂ����Ŕ������Ă������悤�ȏ��������Ă܂��B���߂�Ȃ���
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 5)
        {
            Destroy();
            // ��莞�Ԍo������e�������悤��
        }
    }

    private void Destroy() // �e�������֐� ��s���������ĂȂ��̂Ő����Ȃ������悩������������Ȃ�
    {
        Destroy(gameObject);
    }
}
