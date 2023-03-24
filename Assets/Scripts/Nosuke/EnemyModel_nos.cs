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

    float timer;
    EnemyMove_nos moveScript;

    public Transform shotPos;

    private void Start()
    {
        moveScript = GetComponent<EnemyMove_nos>();
    }
    void Update()
    {
        if (skinny)
        {
            LongAttack();
            Debug.Log("‚â‚¹");
        }
    }

    public void LongAttack()
    {
        timer += Time.deltaTime;
        if(timer >= 1)
        {
            GameObject ammoModel = Instantiate(Resources.Load<GameObject>("Prefabs/EnemyAmmo"),shotPos);
            AmmoScript_nos ammoScript = ammoModel.AddComponent<AmmoScript_nos>();
            ammoScript.atk = enemyAtk;
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
