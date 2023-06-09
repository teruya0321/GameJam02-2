using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene_nos : MonoBehaviour
{
    // ゲームの機能が少ないので、シーン遷移用スクリプトを流用できるように書いてます
    public Text weight;
    public Text BMI;
    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        // メインシーンから移動したらカーソルを動かせるように

        if(weight != null)
        {
            weight.text = PlayerControl_Nat.size + "kg";
            // 餓死した時は体重がマイナスになることもあるが正常な動作である
            // 余談 体重がマイナスって何?ブラックホール?
        }
        if(BMI != null)
        {
            float bmi = PlayerControl_Nat.size /= 3.0625f;
            BMI.text = bmi.ToString("N2");
            // BMIの計算式は体重*身長の二乗。身長は175cmと仮定して計算している
        }
    }
    public void MainScene()
    {
        SceneManager.LoadScene("Map");
        // メインシーンへの移動
    }
    public void BackTitle()
    {
        SceneManager.LoadScene("Title");
        // タイトルシーンへの移動
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();//ゲームプレイ終了
#endif
            // ゲーム終了用のスクリプト コピペしてます
        }
    }
}
