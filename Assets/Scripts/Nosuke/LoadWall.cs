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
            Debug.Log("第一の壁消失！");
        }

        if(timer >= 120)
        {
            walls[1].SetActive(false);
            Debug.Log("第二の壁消失！");
        }

        if(timer >= 180)
        {
            walls[0].SetActive(false);
            Debug.Log("第三の壁消失！");
        }
    }
}
