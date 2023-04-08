using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove_nos : MonoBehaviour
{
    GameObject player;
    // プレイヤーオブジェクト
    public float distance = 10;
    // 距離を測る数値
    CharacterController ChaCon;
    // 動かすためのコライダー
    Vector3 moveDirection = Vector3.zero;

    public float enemGravity;
    public float enemSpeedZ;
    public float enemSpeedRot;
    public float enemJump;
    // それぞれの敵の数値
    float enemTimer = 0;
    int enemAction = 5;

    public float enemTimerRimit;
    public float enemTimerRandom;

    bool isLookPlayer = false;

    EnemyModel_nos model;

    float raytimer;

    float timer;

    void Start()
    {
        Application.targetFrameRate = 60;
        // 消してもいいよねこれ
        ChaCon = GetComponent<CharacterController>();
        model = GetComponent<EnemyModel_nos>();

        player = GameObject.Find("MainCharaAnim");
        // プレイヤーを設定
        if(model.speed == 1)
        {
            enemSpeedZ /= 2;
            
        }
        else if(model.speed == 3)
        {
            enemSpeedZ *= 2;
        }
        // キャラによってスピードを三段階に分ける
    }
    private void Update()
    {
        // Raycastの判定
        Ray rayPosition = new Ray(transform.position, transform.forward.normalized * distance);
        RaycastHit raycastHit;
        Debug.DrawRay(transform.position, transform.forward.normalized * distance, Color.red);
        if (Physics.Raycast(rayPosition, out raycastHit, distance))
        {
            float posdistance = Vector3.Distance(transform.position,raycastHit.transform.position);
            
            //Debug.Log("当たったよ");
            if (raycastHit.collider.gameObject.tag == "Player")
            {
                if(posdistance <= 2 && model.skinny == true)
                {
                    timer += Time.deltaTime;
                    if(timer >= 1)
                    {
                        raycastHit.collider.gameObject.GetComponent<PlayerControl_Nat>().hp += model.enemyAtk;
                        timer = 0;
                    }
                }
                else
                {
                    timer = 0;
                }
                //isLookPlayer = true;
                // 当たっている間は追いかける設定をtrueに
                raytimer = 0;
                Debug.Log("当たったよ");
            }
            else
            {
                //isLookPlayer = false;
                // 当たっていなければfalseにする
                raytimer += Time.deltaTime;
                //Debug.Log("当たってないよ");
            }
            
        }

        if(raytimer <= 2)
        {
            isLookPlayer = true;
        }
        else
        {
            isLookPlayer = false;
        }
        enemTimer += Time.deltaTime;
        // 敵のランダム行動用のタイマー
        if (enemTimer > enemTimerRimit + enemTimerRandom && !isLookPlayer)
        {
            // タイマーが一定時間経って、なおかつプレイヤーを見つけていなかったら、次の行動に移る
            enemActions();
        }

        //移動落下処理
        moveDirection.y -= enemGravity * Time.deltaTime;
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        ChaCon.Move(globalDirection * Time.deltaTime);

        if (ChaCon.isGrounded)
        {
            moveDirection.y = 0;
        }
        // プレイヤーを見つけていたら
        if (isLookPlayer)
        {
            FoundPlayer();
            // 敵が遠距離攻撃できるキャラなら
            if (model.skinny == false)
            {
                model.LongAttack();
            }
        }
    }

    void FoundPlayer()
    {
        // 対象物と自分自身の座標からベクトルを算出
        Vector3 vector3 = player.transform.position - this.transform.position;
        // もし上下方向の回転はしないようにしたければ以下のようにする。
        // vector3.y = 0f;

        // Quaternion(回転値)を取得
        Quaternion quaternion = Quaternion.LookRotation(vector3);
        // 算出した回転値をこのゲームオブジェクトのrotationに代入
        this.transform.rotation = quaternion;

        moveDirection.z = enemSpeedZ * Time.deltaTime * 4;
    }

    

    // 敵のランダム行動を決める関数
    void enemActions()
    {
        //string actWord;
        // 行動をランダムに決める
        switch (enemAction)
        {
            case 0:
                //actWord = "前進";
                moveDirection.z = enemSpeedZ * Time.deltaTime;
                float moveWait0 = 3;
                if (enemTimer > moveWait0) enemActionReset();
                break;
            case 1:
                //actWord = "右旋回";
                transform.Rotate(0, enemSpeedRot * Time.deltaTime, 0);
                float moveWait1 = 2f;
                if (enemTimer > moveWait1) enemActionReset();
                break;
            case 2:
                //actWord = "左旋回";
                transform.Rotate(0, enemSpeedRot * Time.deltaTime * -1, 0);
                float moveWait2 = 2f;
                if (enemTimer > moveWait2) enemActionReset();
                break;
            case 3:
                //actWord = "振り返り";
                transform.Rotate(0, enemSpeedRot * Time.deltaTime * 2, 0);
                float moveWait3 = 2.5f;
                if (enemTimer > moveWait3) enemActionReset();
                break;
            case 4:
                //actWord = "ダッシュ";
                moveDirection.z = enemSpeedZ * Time.deltaTime * 3;
                float moveWait4 = 1;
                if (enemTimer > moveWait4) enemActionReset();
                break;
            case 5:
                //actWord = "待機";
                moveDirection.z *= 0.6f;
                float moveWait5 = 5;
                if (enemTimer > moveWait5) enemActionReset();
                break;
            case 6:
                //actWord = "ジャンプ";
                moveDirection.y = enemJump * Time.deltaTime;
                enemActionReset();
                break;

            default:
                //actWord = "Default";
                break;
        }
        //Debug.Log(actWord);
    }

    // 行動を決めたあとのタイマーのリセットと次の行動パターンを決める関数
    void enemActionReset()
    {
        enemAction = Random.Range(0, 7);
        enemTimer = 0;//タイマーリセット
    }
}
