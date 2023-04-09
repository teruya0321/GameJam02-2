using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene_nos : MonoBehaviour
{
    public Text weight;
    public void Start()
    {
        Cursor.visible = true;

        Cursor.lockState = CursorLockMode.None;
        if(weight != null)
        {
            weight.text = PlayerControl_Nat.size + "kg";
        }
    }
    public void MainScene()
    {
        SceneManager.LoadScene("Map");
    }
    public void BackTitle()
    {
        SceneManager.LoadScene("Title");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
        }
    }
}
