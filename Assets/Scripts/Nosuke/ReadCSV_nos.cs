using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ReadCSV_nos : MonoBehaviour
{
    TextAsset csvFile; // CSV�t�@�C��
    List<string[]> csvDatas = new List<string[]>(); // CSV�̒��g�����郊�X�g;
    int i = 1;
    //public EnemyData_nos data;
    List<string[]> enemyDatas = new List<string[]>();

    public int enemyID;
    public string enemyName;
    public int enemyAtk;
    public int enemyHp;
    private void Awake()
    {
        csvFile = Resources.Load("CSVs/test2") as TextAsset; // Resouces����CSV�ǂݍ���
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StringReader reader = new StringReader(csvFile.text);
            // , �ŕ�������s���ǂݍ���
            // ���X�g�ɒǉ����Ă���
            while (reader.Peek() != -1) // reader.Peaek��-1�ɂȂ�܂�
            {
                string line = reader.ReadLine(); // ��s���ǂݍ���
                csvDatas.Add(line.Split(',')); // , ��؂�Ń��X�g�ɒǉ�s
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