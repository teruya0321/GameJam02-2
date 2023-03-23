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
    public bool fatDrop;

    private void Update()
    {
        
    }

    void IsDead()
    {
        if (fatDrop)
        {
            int i = Random.Range(1, 11);

            if(i <= 3)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/FatItem"));
            }
        }
        Destroy(gameObject);
    }
}
