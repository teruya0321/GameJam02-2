using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ReadCSV_nos : MonoBehaviour
{
    TextAsset csvFile; // CSVファイル
    List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト;
    int i = 1;
    //public EnemyData_nos data;
    List<string[]> enemyDatas = new List<string[]>();

    public int enemyID;
    public string enemyName;
    public int enemyAtk;
    public int enemyHp;
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
            
            GameObject enemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemy" + enemyID));
            EnemyModel_nos ene_script = enemy.AddComponent<EnemyModel_nos>();
            ene_script.enemyID = enemyID;
            ene_script.enemyName = enemyName;
            ene_script.enemyAtk = enemyAtk;
            ene_script.enemyHp = enemyHp;
            //data.DataDisplay();
        }
    }
}