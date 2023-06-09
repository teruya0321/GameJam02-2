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
        // 遠距離攻撃の弾が出る位置を指定
        if (lastBoss && timer <= 0.1)
        {
            enemyAtk *= -1;
            // ラスボスは体重を減らすこともできるので、攻撃ごとに増やすか減らすかが切り替わるように
            // ぶっちゃけbool型で下にあるタイマーリセットの時に切り替えるようにした方が視覚的にわかりやすいとは思う
        }
        if(enemyHp <= 0)
        {
            // HPが0になると倒される処理をする
            IsDead();
        }
    }
     
    public void damage(int damage)
    {
        // 敵のHPを減らす処理
        enemyHp--;
    }
    
    public void LongAttack() // 遠距離攻撃の関数
    {
        timer += Time.deltaTime;
        if(timer >= 1)
        {
            GameObject ammoModel = Instantiate(Resources.Load<GameObject>("Prefabs/EnemyAmmo"),shotPos,Quaternion.identity);
            Vector3 direction = transform.forward;
            ammoModel.AddComponent<Rigidbody>().AddForce(direction * 30, ForceMode.Impulse);
            // 弾を生成、自分の正面に弾き飛ばす
            AmmoScript_nos ammoScript = ammoModel.AddComponent<AmmoScript_nos>();
            ammoScript.atk = enemyAtk;
            // 生成した弾に弾用のスクリプトを追加、攻撃力もここで設定
            timer = 0;
        }
    }
    void IsDead() // 敵が死んだ際に起動される関数
    {
        // dropItemsという変数で何がドロップするのか敵によって切り替えている
        if(dropItems == 1)
        {
            int i = Random.Range(1, 11);
            // 30％で拾ったら体重が増加するアイテムをドロップするように
            if (i <= 3)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/FatItem"));
            }
        }
        else if(dropItems == 2)
        {
            // 中ボスはミニガンをドロップするように
            Instantiate(Resources.Load<GameObject>("Prefabs/MiniGun"));
        }
        Destroy(gameObject);
        // Objectを消去
    }
}
