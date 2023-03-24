using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove_nos : MonoBehaviour
{
    public GameObject player;
    public float distance = 10;

    CharacterController ChaCon;
    Vector3 moveDirection = Vector3.zero;

    public float enemGravity;
    public float enemSpeedZ;
    public float enemSpeedRot;
    public float enemJump;

    float enemTimer = 0;
    int enemAction = 5;
    public float enemTimerRimit;
    public float enemTimerRandom;

    bool isLookPlayer = false;

    EnemyModel_nos model;

    

    void Start()
    {
        Application.targetFrameRate = 60;
        ChaCon = GetComponent<CharacterController>();
        model = GetComponent<EnemyModel_nos>();
        if(model.speed == 1)
        {
            enemSpeedZ /= 2;
        }
        else if(model.speed == 3)
        {
            enemSpeedZ *= 2;
        }
    }
    private void Update()
    {
        Ray rayPosition = new Ray(transform.position, transform.forward.normalized * distance);
        RaycastHit raycastHit;
        Debug.DrawRay(transform.position, transform.forward.normalized * distance, Color.red);
        if (Physics.Raycast(rayPosition, out raycastHit, distance))
        {
            //Debug.Log("当たったよ");
            if (raycastHit.collider.CompareTag("Player"))
            {
                isLookPlayer = true;
                Debug.Log("当たったよ");
            }
            else
            {
                isLookPlayer = false;
                Debug.Log("当たってないよ");
            }
        }
        enemTimer += Time.deltaTime;
        if (enemTimer > enemTimerRimit + enemTimerRandom && !isLookPlayer)
        {
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
        if (isLookPlayer)
        {
            FoundPlayer();
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

    void enemActions()
    {
        string actWord;

        switch (enemAction)
        {
            case 0:
                actWord = "前進";
                moveDirection.z = enemSpeedZ * Time.deltaTime;
                float moveWait0 = 3;
                if (enemTimer > moveWait0) enemActionReset();
                break;
            case 1:
                actWord = "右旋回";
                transform.Rotate(0, enemSpeedRot * Time.deltaTime, 0);
                float moveWait1 = 2f;
                if (enemTimer > moveWait1) enemActionReset();
                break;
            case 2:
                actWord = "左旋回";
                transform.Rotate(0, enemSpeedRot * Time.deltaTime * -1, 0);
                float moveWait2 = 2f;
                if (enemTimer > moveWait2) enemActionReset();
                break;
            case 3:
                actWord = "振り返り";
                transform.Rotate(0, enemSpeedRot * Time.deltaTime * 2, 0);
                float moveWait3 = 2.5f;
                if (enemTimer > moveWait3) enemActionReset();
                break;
            case 4:
                actWord = "ダッシュ";
                moveDirection.z = enemSpeedZ * Time.deltaTime * 3;
                float moveWait4 = 1;
                if (enemTimer > moveWait4) enemActionReset();
                break;
            case 5:
                actWord = "待機";
                moveDirection.z *= 0.6f;
                float moveWait5 = 5;
                if (enemTimer > moveWait5) enemActionReset();
                break;
            case 6:
                actWord = "ジャンプ";
                moveDirection.y = enemJump * Time.deltaTime;
                enemActionReset();
                break;

            default:
                actWord = "Default";
                break;
        }
        Debug.Log(actWord);
    }
    void enemActionReset()
    {
        enemAction = Random.Range(0, 7);
        enemTimer = 0;//タイマーリセット
    }
    /* //プレイヤー
    public GameObject player;
    private NavMeshAgent navMeshAgent;
    //ランダムに方向を変えて移動
    private float chargeTime;
    private float timeCount;
    //rayの距離
    public float distance;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        chargeTime = 3;
    }
    void Update()
    {
        Ray rayPosition = new Ray(transform.position, transform.forward.normalized * distance);
        RaycastHit raycasthit;
        Debug.DrawRay(transform.position, transform.forward.normalized * distance, Color.red);
        if (Physics.Raycast(rayPosition, out raycasthit, distance))
        {
            if (raycasthit.collider.CompareTag("Player"))
            {
                Debug.Log("プレイヤーに当たった");
                navMeshAgent.destination = player.transform.position;
            }
        }
        else
        {
            Debug.Log("探し中");
            //方向転換の時間経過
            timeCount += Time.deltaTime;
            //前進みます
            transform.position += transform.forward * Time.deltaTime;
        }
        //一定の時間経過で方向転換
        if (timeCount > chargeTime)
        {
            //ランダムに角度変更
            Vector3 course = new Vector3(0, UnityEngine.Random.Range(0, 360), 0);
            transform.localRotation = Quaternion.Euler(course);
            //0に戻す
            timeCount = 0;
        }
    }*/


}
