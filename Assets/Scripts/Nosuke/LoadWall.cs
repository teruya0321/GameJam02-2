using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadWall : MonoBehaviour
{
    float timer;

    public GameObject[] walls;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 60)
        {
            walls[0].SetActive(false);
            Debug.Log("���̕Ǐ����I");
        }

        if(timer >= 120)
        {
            walls[1].SetActive(false);
            Debug.Log("���̕Ǐ����I");
        }

        if(timer >= 180)
        {
            walls[0].SetActive(false);
            Debug.Log("��O�̕Ǐ����I");
        }
    }
}
