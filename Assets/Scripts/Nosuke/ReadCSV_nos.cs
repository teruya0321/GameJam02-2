using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ReadCSV_nos : MonoBehaviour
{
    TextAsset csvFile; // CSVファイル
    List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト;
    int i;

    public GameObject gamemanejar;
    LoadWall loadWall;

    public int wallPhase;

    public int enemyID;
    public string enemyName;
    public int enemyAtk;
    public int enemyHp;
    public string speed;
    public string isFatorSkinny;
    public string drops;
    private void Awake()
    {
        csvFile = Resources.Load("CSVs/EnemyDate") as TextAsset; // Resouces下のCSV読み込み
        loadWall = gamemanejar.GetComponent<LoadWall>();
    }
    private void Update()
    {
        if(loadWall.wallCount == wallPhase)
        {
            StringReader reader = new StringReader(csvFile.text);
            // , で分割しつつ一行ずつ読み込み
            // リストに追加していく
            while (reader.Peek() != -1) // reader.Peaekが-1になるまで
            {
                string line = reader.ReadLine(); // 一行ずつ読み込み
                csvDatas.Add(line.Split(',')); // , 区切りでリストに追加s
            }

            if (loadWall.wallCount == 0)
            {
                i = Random.Range(0, 6);
            }
            else if (loadWall.wallCount == 1)
            {
                i = Random.Range(0, 7);
            }
            else if (loadWall.wallCount == 2)
            {
                i = Random.Range(0, 8);
            }
            else
            {
                i = Random.Range(0, 8);
            }

            enemyID = int.Parse(csvDatas[i][0]);
            enemyName = csvDatas[i][1];
            enemyAtk = int.Parse(csvDatas[i][2]);
            enemyHp = int.Parse(csvDatas[i][3]);
            speed = csvDatas[i][4];
            isFatorSkinny = csvDatas[i][5];
            drops = csvDatas[i][6];

            GameObject enemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemy" + enemyID));
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

            ene_move.distance = 10;
            ene_move.enemGravity = 10;
            ene_move.enemSpeedZ = 50;
            ene_move.enemSpeedRot = 100;
            ene_move.enemJump = 500;
            ene_move.enemTimerRimit = 3;
            ene_move.enemTimerRandom = 3;
        }
    }
}