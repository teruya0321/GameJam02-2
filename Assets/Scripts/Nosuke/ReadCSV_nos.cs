using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ReadCSV_nos : MonoBehaviour
{
    TextAsset csvFile; // CSVファイル
    List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト;
    int i;

    Vector3 pos;

    public bool firstSpawn = false;
    public int wallPhase;

    public int enemyID;
    public string enemyName;
    public int enemyAtk;
    public int enemyHp;
    public string speed;
    public string isFatorSkinny;
    public string drops;

    public bool boss;
    private void Awake()
    {
        csvFile = Resources.Load("CSVs/EnemyDate3") as TextAsset; // Resouces下のCSV読み込み
        pos = gameObject.transform.position;
    }

    private void Start()
    {
        if (firstSpawn)
        {
            SpawnEnemy();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "areaCylinder")
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        StringReader reader = new StringReader(csvFile.text);
        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            csvDatas.Add(line.Split(',')); // , 区切りでリストに追加s
        }

        if (boss)
        {
            i = Random.Range(11, 13);
        }
        else
        {
            i = Random.Range(1, 11);
        }

        enemyID = int.Parse(csvDatas[i][0]);
        enemyName = csvDatas[i][1];
        enemyAtk = int.Parse(csvDatas[i][2]);
        enemyHp = int.Parse(csvDatas[i][3]);
        speed = csvDatas[i][4];
        isFatorSkinny = csvDatas[i][5];
        drops = csvDatas[i][6];

        GameObject enemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemy" + enemyID), pos, Quaternion.identity);
        EnemyModel_nos ene_script = enemy.AddComponent<EnemyModel_nos>();

        ene_script.enemyID = enemyID;
        ene_script.enemyName = enemyName;
        ene_script.enemyAtk = enemyAtk;
        ene_script.enemyHp = enemyHp;

        if (speed == "遅い")
        {
            ene_script.speed = 1;
        }
        else if (speed == "普通")
        {
            ene_script.speed = 2;
        }
        else if (speed == "早い")
        {
            ene_script.speed = 3;
        }

        if (isFatorSkinny == "マッチョにさせる")
        {
            ene_script.enemyAtk *= 1;
        }
        else if (isFatorSkinny == "痩せさせる")
        {
            ene_script.enemyAtk *= -1;
            ene_script.skinny = true;
        }
        else
        {
            ene_script.lastBoss = true;
        }

        if (drops == "30％で太るアイテムを出す")
        {
            ene_script.dropItems = 1;
        }
        else if (drops == "武器泥")
        {
            ene_script.dropItems = 2;
        }

        EnemyMove_nos ene_move = enemy.AddComponent<EnemyMove_nos>();
        enemy.AddComponent<DestroyObject_nos>();

        ene_move.distance = 10;
        ene_move.enemGravity = 10;
        ene_move.enemSpeedZ = 50;
        ene_move.enemSpeedRot = 100;
        ene_move.enemJump = 500;
        ene_move.enemTimerRimit = 3;
        ene_move.enemTimerRandom = 3;

        Destroy(gameObject);
    }
}