using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene_nos : MonoBehaviour
{
    // �Q�[���̋@�\�����Ȃ��̂ŁA�V�[���J�ڗp�X�N���v�g�𗬗p�ł���悤�ɏ����Ă܂�
    public Text weight;
    public Text BMI;
    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        // ���C���V�[������ړ�������J�[�\���𓮂�����悤��

        if(weight != null)
        {
            weight.text = PlayerControl_Nat.size + "kg";
            // �쎀�������͑̏d���}�C�i�X�ɂȂ邱�Ƃ����邪����ȓ���ł���
            // �]�k �̏d���}�C�i�X���ĉ�?�u���b�N�z�[��?
        }
        if(BMI != null)
        {
            float bmi = PlayerControl_Nat.size /= 3.0625f;
            BMI.text = bmi.ToString("N2");
            // BMI�̌v�Z���͑̏d*�g���̓��B�g����175cm�Ɖ��肵�Čv�Z���Ă���
        }
    }
    public void MainScene()
    {
        SceneManager.LoadScene("Map");
        // ���C���V�[���ւ̈ړ�
    }
    public void BackTitle()
    {
        SceneManager.LoadScene("Title");
        // �^�C�g���V�[���ւ̈ړ�
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();//�Q�[���v���C�I��
#endif
            // �Q�[���I���p�̃X�N���v�g �R�s�y���Ă܂�
        }
    }
}
