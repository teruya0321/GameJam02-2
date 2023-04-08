using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl_Nat : MonoBehaviour
{
    CharacterController controller;
    Vector3 movedir = Vector3.zero;
    Vector3 roteuler;
    public float speed;

    public GameObject[] bulletPrefab;
    GameObject bullet;

    public GameObject[] Barrel;
    public GameObject[] finFanelbarrel;

    public float ballspeed;

    public GameObject Player;

    /*public int limit;
    public int maxlimit;
    public int minlimit;*/

    public Transform parentTran;
    public GameObject arm;
    public GameObject BackPack;

    public int count;

    public int Situation = 0;//0=�f��A1�`=���킠��

    ChildrenScript childrenScript;

    public int PSbulletpower;
    public int MPbulletpower;
    public int MGbulletpower;
    public int ARbulletpower;
    public int LMGbulletpower;
    public int MNGbulletpower;
    public int FinFanelpower;

    float PScount;
    float MPcount;
    float MGcount;
    float ARcount;
    float LMGcount;
    float MNGcount;
    float FinFanelcount;

    public GameObject HitObject;

    public int hp = 0;

    public SkinnedMeshRenderer blendshapeRenderer;

    Animator anim;

    public int hpLowerTime = 2;
    float hpTimer;

    bool onCursor = true;

    public Text sizeText;

    public Image PSIcon;
    public Image MPIcon;
    public Image MGIcon;
    public Image ARIcon;
    public Image LMGIcon;
    public Image MNGIcon;
    public Image FinFanelIcon;

    public Image FatIcon;
    public Image NormalIcon;
    public Image SkinnyIcon;

    void Start()
    {
        PScount = 0;
        MPcount = 0;
        MGcount = 0;
        ARcount = 0;
        LMGcount = 0;
        MNGcount = 0;
        FinFanelcount = 0;

        PSIcon.enabled = false;
        MPIcon.enabled = false;
        MGIcon.enabled = false;
        ARIcon.enabled = false;
        LMGIcon.enabled = false;
        MNGIcon.enabled = false;
        FinFanelIcon.enabled = false;

        count = 0;

        controller = GetComponent<CharacterController>();

        roteuler = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);//���_�ړ��̓z�ł�

        //limit = 0;

        anim = GetComponent<Animator>();

        onCursor = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            onCursor = !onCursor;
        }

        if (onCursor)
        {
            Cursor.visible = false;

            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;

            Cursor.lockState = CursorLockMode.None;
        }

        float size = hp + 100 * 0.7f;
        sizeText.text = size.ToString("N0");

        hpTimer += Time.deltaTime;

        if(hpTimer >= hpLowerTime)
        {
            hp--;
            hpTimer = 0;
        }

        if(hp > 100)
        {
            SceneManager.LoadScene("FatEnd");
        }
        if(hp <= 100 && hp > 25)
        {
            FatIcon.enabled = true;
            NormalIcon.enabled = false;
            SkinnyIcon.enabled = false;
        }
        if(hp <= 25 && hp >= -25)
        {
            FatIcon.enabled = false;
            NormalIcon.enabled = true;
            SkinnyIcon.enabled = false;
        }
        if(hp < -25 && hp >= -100)
        {
            FatIcon.enabled = false;
            NormalIcon.enabled = false;
            SkinnyIcon.enabled = true;
        }
        if(hp < -100)
        {
            SceneManager.LoadScene("SkinnyEnd");
        }
        if(hp > 0)
        {
            blendshapeRenderer.SetBlendShapeWeight(0, hp);
            blendshapeRenderer.SetBlendShapeWeight(1, 0);
        }
        else if(hp < 0)
        {
            blendshapeRenderer.SetBlendShapeWeight(1, hp * -1);
            blendshapeRenderer.SetBlendShapeWeight(0, 0);
        }
        if(hp < -100)
        {
            Debug.LogWarning("You Dead");
            anim.SetBool("Idel", false);
            anim.SetBool("Run", false);
            anim.SetBool("Jump", false);
            anim.SetBool("backStep", false);
            anim.SetBool("Dead", true);
        }

        PScount += Time.deltaTime;
        MPcount += Time.deltaTime;
        MGcount += Time.deltaTime;
        ARcount += Time.deltaTime;
        LMGcount += Time.deltaTime;
        MNGcount += Time.deltaTime;
        FinFanelcount += Time.deltaTime;

        Arm();

        //limit ++;

        /*limit = System.Math.Min(limit, maxlimit);
        limit = System.Math.Max(limit, minlimit);*/

        if (controller.isGrounded)//�v���C���[�̈ړ������ɂȂ��Ă���܂�
        {
            movedir.z = Input.GetAxisRaw("Vertical") * speed;

            if(movedir.z == 10 || movedir.z == -10)
            {
                anim.SetBool("Idel", false);
                anim.SetBool("Run", true);
            }
            else if(movedir.z == 0)
            {
                anim.SetBool("Idel", true);
                anim.SetBool("Run", false);
            }

            movedir.x = Input.GetAxisRaw("Horizontal") * speed;

            if (movedir.x == 10 || movedir.x == -10)
            {
                anim.SetBool("Idel", false);
                anim.SetBool("Run", true);
            }

            if (Input.GetMouseButtonDown(1))//�E�N���b�N�ŃW�����v
            {
                movedir.y = 10f;
                if(movedir.y <= 1)
                {
                    Debug.Log(movedir.y);
                    anim.SetBool("Idel", false);
                    anim.SetBool("Jump", true);
                }
                else if (movedir.y >= 0)
                {
                    Debug.Log(movedir.y);
                    anim.SetBool("Idel", true);
                    anim.SetBool("Jump", false);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.S))//S���_�u���^�b�v�Ńo�b�NStep
        {
            count++;
            Invoke("Step", 0.2f);
        }

        movedir.y -= 20f * Time.deltaTime;

        Vector3 globaldir = transform.TransformDirection(movedir);
        controller.Move(globaldir * Time.deltaTime);

        if (controller.isGrounded)
        {
            movedir.y = 0;
        }

        float mouseInputX = Input.GetAxis("Mouse X");//���̎��_�ړ�

        roteuler = new Vector3(0, roteuler.y + mouseInputX * 3, 0f);
        transform.localEulerAngles = roteuler;
    }

    void Step()//�o�b�NStep����
    {
        if (count != 2)
        {
            count = 0;
        }
        else if (count <= 2)
        {
            movedir.y = 5f;
            movedir.z = -15f;
            anim.SetBool("Idel", false);
            anim.SetBool("backStep", true);
            Invoke("reset", 0.3f);
        }
    }

    void reset()
    {
        count = 0; 
    }

    void Arm()//��Ԃɉ����Ă̕���̐؂�ւ�����
    {
        if (Input.GetMouseButtonDown(0) && Situation == 1)//���N���b�N��Ray����
        {
            Pistol();
        }

        if (Input.GetMouseButton(0) && Situation == 2)//���N���b�N��Ray����
        {
            MachinePistol();
        }
        
        if (Input.GetMouseButton(0) && Situation == 3)//���N���b�N��Ray����
        {
            MG();
        }

        if (Input.GetMouseButton(0) && Situation == 4)//���N���b�N��Ray����
        {
            AR();
        }

        if (Input.GetMouseButton(0) && Situation == 5)//���N���b�N��Ray����
        {
            LMG();
        }

        if (Input.GetMouseButton(0) && Situation == 6)//���N���b�N��Ray����
        {
            MiniGun();
        }
        
        if (Input.GetMouseButton(0) && Situation == 7)//���N���b�N��Ray����
        {
            FinFanel();   
        }
    }

    void Pistol()//Pistol����
    {
        if (PScount >= 1)
        {
            Ray ray = new Ray(Barrel[0].transform.position, Player.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Instantiate(HitObject, hit.transform.position, Quaternion.identity);
                    hit.transform.GetComponent<EnemyModel_nos>().damage(PSbulletpower);

                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                    Debug.Log("������܂���");
                    PScount = 0.7f;
                    /*bullet = Instantiate(bulletPrefab[0], Barrel[0].transform.position, Quaternion.identity);
                    Vector3 worldDir = ray.direction;
                    bullet.GetComponent<HandGunBulletScript>().Shot(worldDir * ballspeed);
                    Destroy(bullet, 1f);*/
                }
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                PScount = 0.7f;
                /*bullet = Instantiate(bulletPrefab[0], Barrel[0].transform.position, Quaternion.identity);
                Vector3 worldDir = ray.direction;
                bullet.GetComponent<HandGunBulletScript>().Shot(worldDir * ballspeed);
                Destroy(bullet, 1f);*/
            }
        }
    }

    void MachinePistol()//�}�V��Pistol����
    {
        if (MPcount >= 1)
        {
            Ray ray = new Ray(Barrel[1].transform.position, Player.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Instantiate(HitObject, hit.point, Quaternion.identity);
                    hit.transform.GetComponent<EnemyModel_nos>().damage(MPbulletpower);

                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                    Debug.Log("������܂���");
                    MPcount = 0.85f;
                    /*bullet = Instantiate(bulletPrefab[1], Barrel[1].transform.position, Quaternion.identity);
                    Vector3 worldDir = ray.direction;
                    bullet.GetComponent<MachinePistolBulletScript>().Shot(worldDir * ballspeed);
                    Destroy(bullet, 1f);*/
                }
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                MPcount = 0.85f;
                /*bullet = Instantiate(bulletPrefab[1], Barrel[1].transform.position, Quaternion.identity);
                Vector3 worldDir = ray.direction;
                bullet.GetComponent<MachinePistolBulletScript>().Shot(worldDir * ballspeed);
                Destroy(bullet, 1f);*/
            }
        }
    }

    void MG()//100mm�}�V���K������
    {
        if (MGcount >= 1)
        {
            Ray ray = new Ray(Barrel[2].transform.position, Player.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Instantiate(HitObject, hit.point, Quaternion.identity);
                    hit.transform.GetComponent<EnemyModel_nos>().damage(MGbulletpower);

                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                    Debug.Log("������܂���");
                    MGcount = 0.75f;
                    /*bullet = Instantiate(bulletPrefab[1], Barrel[1].transform.position, Quaternion.identity);
                    Vector3 worldDir = ray.direction;
                    bullet.GetComponent<MachinePistolBulletScript>().Shot(worldDir * ballspeed);
                    Destroy(bullet, 1f);*/
                }
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                MGcount = 0.75f;
                /*bullet = Instantiate(bulletPrefab[1], Barrel[1].transform.position, Quaternion.identity);
                Vector3 worldDir = ray.direction;
                bullet.GetComponent<MachinePistolBulletScript>().Shot(worldDir * ballspeed);
                Destroy(bullet, 1f);*/
            }
        }
    }

    void AR()//�A�T���g���C�t������
    {
        if (ARcount >= 1)
        {
            Ray ray = new Ray(Barrel[3].transform.position, Player.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Instantiate(HitObject, hit.point, Quaternion.identity);
                    hit.transform.GetComponent<EnemyModel_nos>().damage(ARbulletpower);

                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                    Debug.Log("������܂���");
                    ARcount = 0.85f;
                    /*bullet = Instantiate(bulletPrefab[2], Barrel[2].transform.position, Quaternion.identity);
                    Vector3 worldDir = ray.direction;
                    bullet.GetComponent<ARBulletScript>().Shot(worldDir * ballspeed);
                    Destroy(bullet, 1f); */
                }
                
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                ARcount = 0.85f;
                /*bullet = Instantiate(bulletPrefab[2], Barrel[2].transform.position, Quaternion.identity);
                Vector3 worldDir = ray.direction;
                bullet.GetComponent<ARBulletScript>().Shot(worldDir * ballspeed);
                Destroy(bullet, 1f);*/
            }
        }
    }

    void LMG()//���C�g�}�V���K������
    {
        if(LMGcount >= 1)
        {
            Ray ray = new Ray(Barrel[4].transform.position, Player.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Instantiate(HitObject, hit.point, Quaternion.identity);
                    hit.transform.GetComponent<EnemyModel_nos>().damage(LMGbulletpower);

                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                    Debug.Log("������܂���");
                    LMGcount = 0.8f;
                    /*bullet = Instantiate(bulletPrefab[3], Barrel[3].transform.position, Quaternion.identity);
                    Vector3 worldDir = ray.direction;
                    bullet.GetComponent<LMGBulletScript>().Shot(worldDir * ballspeed);
                    Destroy(bullet, 1f);*/
                }
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                LMGcount = 0.8f;
                /*bullet = Instantiate(bulletPrefab[3], Barrel[3].transform.position, Quaternion.identity);
                Vector3 worldDir = ray.direction;
                bullet.GetComponent<LMGBulletScript>().Shot(worldDir * ballspeed);
                Destroy(bullet, 1f);*/
            }
        }
    }

    void MiniGun()//�~�j�K������
    {
        if (MNGcount >= 1)
        {
            Ray ray = new Ray(Barrel[5].transform.position, Player.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Instantiate(HitObject, hit.point, Quaternion.identity);
                    hit.transform.GetComponent<EnemyModel_nos>().damage(MNGbulletpower);

                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                    Debug.Log("������܂���");
                    MNGcount = 0.95f;
                    /*bullet = Instantiate(bulletPrefab[4], Barrel[4].transform.position, Quaternion.identity);
                    Vector3 worldDir = ray.direction;
                    bullet.GetComponent<MiniGunBulletScript>().Shot(worldDir * ballspeed);
                    Destroy(bullet, 1f);*/
                }
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                MNGcount = 0.95f;
                /*bullet = Instantiate(bulletPrefab[4], Barrel[4].transform.position, Quaternion.identity);
                Vector3 worldDir = ray.direction;
                bullet.GetComponent<MiniGunBulletScript>().Shot(worldDir * ballspeed);
                Destroy(bullet, 1f);*/
            }
        }
    }

    void FinFanel()//�t�B���t�@���l���b!!!
    {
        if (FinFanelcount >= 1)
        {
            Ray ray = new Ray(finFanelbarrel[Random.Range(0, 6)].transform.position, Player.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Instantiate(HitObject, hit.point, Quaternion.identity);
                    hit.transform.GetComponent<EnemyModel_nos>().damage(FinFanelpower);

                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                    Debug.Log("������܂���");
                    FinFanelcount = 0.95f;
                    bullet = Instantiate(bulletPrefab[0], finFanelbarrel[Random.Range(0, 6)].transform.position, Quaternion.identity);
                    Vector3 worldDir = ray.direction;
                    bullet.GetComponent<FinFannelBeamScript>().Shot(worldDir * ballspeed);
                    Destroy(bullet, 0.1f);
                }
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                FinFanelcount = 0.95f;
                /*bullet = Instantiate(bulletPrefab[0], Barrel[5].transform.position, Quaternion.identity);
                Vector3 worldDir = ray.direction;
                bullet.GetComponent<MiniGunBulletScript>().Shot(worldDir * ballspeed);
                Destroy(bullet, 1f);*/
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Pistol")
        {
            if(childrenScript != null)
            {
                childrenScript.remove();
            }

            Situation = 1;
            other.transform.position = arm.transform.position;
            other.gameObject.transform.SetParent(parentTran);
            other.transform.localEulerAngles = Vector3.zero;
            childrenScript = other.gameObject.GetComponent<ChildrenScript>();
            PSIcon.enabled = true;
            MPIcon.enabled = false;
            MGIcon.enabled = false;
            ARIcon.enabled = false;
            LMGIcon.enabled = false;
            MNGIcon.enabled = false;
            FinFanelIcon.enabled = false;
        }
       
        if (other.gameObject.name == "MachinePistol")
        {
            if (childrenScript != null)
            {
                childrenScript.remove();
            }

            Situation = 2;
            other.transform.position = arm.transform.position;
            other.gameObject.transform.SetParent(parentTran);
            other.transform.localEulerAngles = Vector3.zero;
            childrenScript = other.gameObject.GetComponent<ChildrenScript>();
            MPIcon.enabled = true;
            PSIcon.enabled = false;
            MGIcon.enabled = false;
            ARIcon.enabled = false;
            LMGIcon.enabled = false;
            MNGIcon.enabled = false;
            FinFanelIcon.enabled = false;
        }
        
        if (other.gameObject.name == "100mmMachingun02")
        {
            if (childrenScript != null)
            {
                childrenScript.remove();
            }

            Situation = 3;
            other.transform.position = arm.transform.position;
            other.gameObject.transform.SetParent(parentTran);
            other.transform.localEulerAngles = Vector3.zero;
            childrenScript = other.gameObject.GetComponent<ChildrenScript>();
            MGIcon.enabled = true;
            PSIcon.enabled = false;
            MPIcon.enabled = false;
            ARIcon.enabled = false;
            LMGIcon.enabled = false;
            MNGIcon.enabled = false;
            FinFanelIcon.enabled = false;
        }

        if (other.gameObject.name == "AssaultRifle")
        {
            if (childrenScript != null)
            {
                childrenScript.remove();
            }

            Situation = 4;
            other.transform.position = arm.transform.position;
            other.gameObject.transform.SetParent(parentTran);
            other.transform.localEulerAngles = Vector3.zero;
            childrenScript = other.gameObject.GetComponent<ChildrenScript>();
            ARIcon.enabled = true;
            PSIcon.enabled = false;
            MPIcon.enabled = false;
            MGIcon.enabled = false;
            LMGIcon.enabled = false;
            MNGIcon.enabled = false;
            FinFanelIcon.enabled = false;
        }

        if (other.gameObject.name == "LMG")
        {
            if (childrenScript != null)
            {
                childrenScript.remove();
            }

            Situation = 5;
            other.transform.position = arm.transform.position;
            other.gameObject.transform.SetParent(parentTran);
            other.transform.localEulerAngles = Vector3.zero;
            childrenScript = other.gameObject.GetComponent<ChildrenScript>();
            LMGIcon.enabled = true;
            PSIcon.enabled = false;
            MPIcon.enabled = false;
            MGIcon.enabled = false;
            ARIcon.enabled = false;
            MNGIcon.enabled = false;
            FinFanelIcon.enabled = false;
        }

        if (other.gameObject.name == "MiniGun")
        {
            if (childrenScript != null)
            {
                childrenScript.remove();
            }

            Situation = 6;
            other.transform.position = arm.transform.position;
            other.gameObject.transform.SetParent(parentTran);
            other.transform.localEulerAngles = Vector3.zero;
            childrenScript = other.gameObject.GetComponent<ChildrenScript>();
            MNGIcon.enabled = true;
            PSIcon.enabled = false;
            MPIcon.enabled = false;
            MGIcon.enabled = false;
            ARIcon.enabled = false;
            LMGIcon.enabled = false;
            FinFanelIcon.enabled = false;
        }

        if (other.gameObject.name == "FinFanel03")
        {
            if (childrenScript != null)
            {
                childrenScript.remove();
            }

            Situation = 7;
            other.transform.position = BackPack.transform.position;
            other.gameObject.transform.SetParent(parentTran);
            other.transform.localEulerAngles = Vector3.zero;
            childrenScript = other.gameObject.GetComponent<ChildrenScript>();
            FinFanelIcon.enabled = true;
            PSIcon.enabled = false;
            MPIcon.enabled = false;
            MGIcon.enabled = false;
            ARIcon.enabled = false;
            LMGIcon.enabled = false;
            MNGIcon.enabled = false;    
        }
    }
}
