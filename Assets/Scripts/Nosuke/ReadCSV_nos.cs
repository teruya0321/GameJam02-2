using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ReadCSV_nos : MonoBehaviour
{
    TextAsset csvFile; // CSV�t�@�C��
    List<string[]> csvDatas = new List<string[]>(); // CSV�̒��g�����郊�X�g;
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
        csvFile = Resources.Load("CSVs/EnemyDate3") as TextAsset; // Resouces����CSV�ǂݍ���
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
        // , �ŕ�������s���ǂݍ���
        // ���X�g�ɒǉ����Ă���
        while (reader.Peek() != -1) // reader.Peaek��-1�ɂȂ�܂�
        {
            string line = reader.ReadLine(); // ��s���ǂݍ���
            csvDatas.Add(line.Split(',')); // , ��؂�Ń��X�g�ɒǉ�s
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

        if (speed == "�x��")
        {
            ene_script.speed = 1;
        }
        else if (speed == "����")
        {
            ene_script.speed = 2;
        }
        else if (speed == "����")
        {
            ene_script.speed = 3;
        }

        if (isFatorSkinny == "�}�b�`���ɂ�����")
        {
            ene_script.enemyAtk *= 1;
        }
        else if (isFatorSkinny == "����������")
        {
            ene_script.enemyAtk *= -1;
            ene_script.skinny = true;
        }
        else
        {
            ene_script.lastBoss = true;
        }

        if (drops == "30���ő���A�C�e�����o��")
        {
            ene_script.dropItems = 1;
        }
        else if (drops == "����D")
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