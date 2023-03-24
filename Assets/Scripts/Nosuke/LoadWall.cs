using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadWall : MonoBehaviour
{
    float timer;

    public GameObject[] walls;

    public int wallCount = 0;
    // Update is called once per frame

    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 60 && timer < 120)
        {
            walls[0].SetActive(false);
            wallCount = 1;
            Debug.Log("���̕Ǐ����I");
        }

        if(timer >= 120 && timer < 180)
        {
            walls[1].SetActive(false);
            wallCount = 2;
            Debug.Log("���̕Ǐ����I");
        }

        if(timer >= 180)
        {
            walls[0].SetActive(false);
            wallCount = 3;
            Debug.Log("��O�̕Ǐ����I");
        }
    }
}
