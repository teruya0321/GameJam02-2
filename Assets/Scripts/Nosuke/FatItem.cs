using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatItem : MonoBehaviour
{
    // �E�����瑾��A�C�e���̃X�N���v�g ���点�Ă���G����30���̊m���Ńh���b�v����
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControl_Nat>().hp += 10;
            Destroy(gameObject);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Player")
        {
            hit.gameObject.GetComponent<PlayerControl_Nat>().hp += 50;
            Destroy(gameObject);
        }
    }

    // �v���C���[�̃��f����Collider��CharacterControler�̓��ނ�Component�����Ă���A�ǂ����Ŕ������Ă���̂��m�F���鎞�Ԃ����������̂ŁA
    // ����̍�łǂ����Ŕ������Ă������悤�ȏ��������Ă܂��B���߂�Ȃ���
}
