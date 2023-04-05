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
    // 敵のデータ

    float timer;

    public Transform shot;
    Vector3 shotPos;
    // 遠距離の敵用の射撃位置
    void Update()
    {
        shotPos.x = transform.position.x;
        shotPos.y = transform.position.y;
        shotPos.z = transform.position.z + 1;
        // 射撃位置を決める
        if (lastBoss && timer <= 0.1)
        {
            enemyAtk *= -1;
        }
        if(enemyHp <= 0)
        {
            // HPが0になると倒される処理をする
            IsDead();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            // 敵のHPを減らす処理
            enemyHp--;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && !skinny)
        {
            collision.gameObject.GetComponent<PlayerControl_Nat>().hp -= enemyAtk;
        }
    }
    public void LongAttack()
    {
        timer += Time.deltaTime;
        if(timer >= 1)
        {
            GameObject ammoModel = Instantiate(Resources.Load<GameObject>("Prefabs/EnemyAmmo"),shotPos,Quaternion.identity);
            Vector3 direction = transform.forward;
            ammoModel.AddComponent<Rigidbody>().AddForce(direction * 30, ForceMode.Impulse);
            AmmoScript_nos ammoScript = ammoModel.AddComponent<AmmoScript_nos>();
            ammoScript.atk = enemyAtk;
            timer = 0;
        }
    }
    void IsDead()
    {
        if(dropItems == 1)
        {
            int i = Random.Range(1, 11);

            if (i <= 3)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/FatItem"));
            }
        }
        else if(dropItems == 2)
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/MiniGun"));
        }
        Destroy(gameObject);
    }
}
