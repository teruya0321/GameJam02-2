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
    // “G‚Ìƒf[ƒ^

    float timer;

    public Transform shot;
    Vector3 shotPos;
    // ‰“‹——£‚Ì“G—p‚ÌËŒ‚ˆÊ’u
    void Update()
    {
        shotPos.x = transform.position.x;
        shotPos.y = transform.position.y;
        shotPos.z = transform.position.z + 1;
        // ËŒ‚ˆÊ’u‚ğŒˆ‚ß‚é
        if (lastBoss && timer <= 0.1)
        {
            enemyAtk *= -1;
        }
        if(enemyHp <= 0)
        {
            // HP‚ª0‚É‚È‚é‚Æ“|‚³‚ê‚éˆ—‚ğ‚·‚é
            IsDead();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            // “G‚ÌHP‚ğŒ¸‚ç‚·ˆ—
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
