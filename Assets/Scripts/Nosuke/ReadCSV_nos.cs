using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ReadCSV_nos : MonoBehaviour
{
    TextAsset csvFile; // CSVファイル
    List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト;
    int i;

    public int enemyID;
    public string enemyName;
    public int enemyAtk;
    public int enemyHp;
    public string speed;
    public string isFatorSkinny;
    public string drops;
    private void Awake()
    {
        csvFile = Resources.Load("CSVs/test2") as TextAsset; // Resouces下のCSV読み込み
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StringReader reader = new StringReader(csvFile.text);
            // , で分割しつつ一行ずつ読み込み
            // リストに追加していく
            while (reader.Peek() != -1) // reader.Peaekが-1になるまで
            {
                string line = reader.ReadLine(); // 一行ずつ読み込み
                csvDatas.Add(line.Split(',')); // , 区切りでリストに追加s
            }
            i = Random.Range(1, 4);
            
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

            if(speed == "遅い")
            {
                ene_script.speed = 1;
            }
            else if(speed == "普通")
            {
                ene_script.speed = 2;
            }
            else if(speed == "早い")
            {
                ene_script.speed = 3;
            }

            if(isFatorSkinny == "マッチョにさせる")
            {
                ene_script.enemyAtk *= 1;
            }
            else if(isFatorSkinny == "痩せさせる")
            {
                ene_script.enemyAtk *= -1;
                ene_script.skinny = true;
            }

            if(drops == "30％で太るアイテムを出す")
            {
                ene_script.dropItems = 1;
            }
            else if(drops == "武器泥")
            {
                ene_script.dropItems = 1;
            }
            //data.DataDisplay();
        }
    }
}