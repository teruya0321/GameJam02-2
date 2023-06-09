using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel_nos : MonoBehaviour
{
    public int enemyID;
    public string enemyName;
    public int enemyAtk;
    public int enemyHp;
    public float speed;
    public bool drops;
    public int dropItems;
    public bool skinny = false;
    public bool lastBoss = false;
    // �G�̃f�[�^

    float timer;

    public Transform shot;
    Vector3 shotPos;
    // �������̓G�p�̎ˌ��ʒu
    void Update()
    {
        shotPos.x = transform.position.x;
        shotPos.y = transform.position.y;
        shotPos.z = transform.position.z + 1;
        // �������U���̒e���o��ʒu���w��
        if (lastBoss && timer <= 0.1)
        {
            enemyAtk *= -1;
            // ���X�{�X�͑̏d�����炷���Ƃ��ł���̂ŁA�U�����Ƃɑ��₷�����炷�����؂�ւ��悤��
            // �Ԃ����Ⴏbool�^�ŉ��ɂ���^�C�}�[���Z�b�g�̎��ɐ؂�ւ���悤�ɂ����������o�I�ɂ킩��₷���Ƃ͎v��
        }
        if(enemyHp <= 0)
        {
            // HP��0�ɂȂ�Ɠ|����鏈��������
            IsDead();
        }
    }
     
    public void damage(int damage)
    {
        // �G��HP�����炷����
        enemyHp--;
    }
    
    public void LongAttack() // �������U���̊֐�
    {
        timer += Time.deltaTime;
        if(timer >= 1)
        {
            GameObject ammoModel = Instantiate(Resources.Load<GameObject>("Prefabs/EnemyAmmo"),shotPos,Quaternion.identity);
            Vector3 direction = transform.forward;
            ammoModel.AddComponent<Rigidbody>().AddForce(direction * 30, ForceMode.Impulse);
            // �e�𐶐��A�����̐��ʂɒe����΂�
            AmmoScript_nos ammoScript = ammoModel.AddComponent<AmmoScript_nos>();
            ammoScript.atk = enemyAtk;
            // ���������e�ɒe�p�̃X�N���v�g��ǉ��A�U���͂������Őݒ�
            timer = 0;
        }
    }
    void IsDead() // �G�����񂾍ۂɋN�������֐�
    {
        // dropItems�Ƃ����ϐ��ŉ����h���b�v����̂��G�ɂ���Đ؂�ւ��Ă���
        if(dropItems == 1)
        {
            int i = Random.Range(1, 11);
            // 30���ŏE������̏d����������A�C�e�����h���b�v����悤��
            if (i <= 3)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/FatItem"));
            }
        }
        else if(dropItems == 2)
        {
            // ���{�X�̓~�j�K�����h���b�v����悤��
            Instantiate(Resources.Load<GameObject>("Prefabs/MiniGun"));
        }
        Destroy(gameObject);
        // Object������
    }
}
