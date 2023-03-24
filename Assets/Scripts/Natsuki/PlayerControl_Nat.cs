using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl_Nat : MonoBehaviour
{
    CharacterController controller;
    Vector3 movedir = Vector3.zero;
    Vector3 roteuler;
    public float speed;

    public GameObject[] bulletPrefab;
    GameObject bullet;

    GameObject[] Barrel;

    public float ballspeed;

    public GameObject Player;

    public int limit;
    public int maxlimit;
    public int minlimit;

    public Transform parentTran;
    public GameObject arm;

    public int count;

    int Situation = 0;//0=素手、1=武器あり

    ChildrenScript childrenScript;

    public int HGbulletpower;
    public int MPbulletpower;
    public int ARbulletpower;
    public int LMGFbulletpower;
    public int MGbulletpower;

    float HGcount;
    float MPcount;
    float ARcount;
    float LMGcount;
    float MNGcount;


    public int hp = 0;

    public SkinnedMeshRenderer blendshapeRenderer;

    

    public int hpLowerTime = 2;
    float hpTimer;
    void Start()
    {
        HGcount = 0;
        MPcount = 0;
        ARcount = 0;
        LMGcount = 0;
        MNGcount = 0;

        count = 0;

        controller = GetComponent<CharacterController>();

        roteuler = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);//視点移動の奴です

        limit = 0;

            
    }
    void Update()
    {
        hpTimer += Time.deltaTime;

        if(hpTimer >= hpLowerTime)
        {
            hp--;
            hpTimer = 0;
        }

        if(hp > 100)
        {
            hp = 100;
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
        }
        HGcount += Time.deltaTime;
        MPcount += Time.deltaTime;
        ARcount += Time.deltaTime;
        LMGcount += Time.deltaTime;
        MNGcount += Time.deltaTime;

        Arm();

        limit ++;

        limit = System.Math.Min(limit, maxlimit);
        limit = System.Math.Max(limit, minlimit);

        if (controller.isGrounded)
        {

            movedir.z = Input.GetAxisRaw("Vertical") * speed;


            movedir.x = Input.GetAxisRaw("Horizontal") * speed;

            if (Input.GetMouseButtonDown(1))//右クリックでジャンプ
            {
                movedir.y = 10f;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            count++;
            Invoke("Step", 0.3f);
        }

        movedir.y -= 20f * Time.deltaTime;

        Vector3 globaldir = transform.TransformDirection(movedir);
        controller.Move(globaldir * Time.deltaTime);

        if (controller.isGrounded)
        {
            movedir.y = 0;
        }

        float mouseInputX = Input.GetAxis("Mouse X");//横の視点移動

        roteuler = new Vector3(0, roteuler.y + mouseInputX * -1, 0f);
        transform.localEulerAngles = roteuler;
    }

    void Step()
    {
        if (count != 2)
        {
            count = 0;
        }
        else if (count <= 2)
        {
            movedir.y = 5f;
            movedir.z = -15f;
            Invoke("reset", 0.3f);
        }
    }

    void reset()
    {
        count = 0; 
    }

    void Arm()
    {
        if (Input.GetMouseButtonDown(0) && Situation == 1)//左クリックでRay発射
        {
            HandGun();
        }

        if (Input.GetMouseButton(0) && Situation == 2)//左クリックでRay発射
        {
            MachinePistol();
        }

        if (Input.GetMouseButton(0) && Situation == 3)//左クリックでRay発射
        {
            AR();
        }

        if (Input.GetMouseButton(0) && Situation == 4)//左クリックでRay発射
        {
            LMG();
        }

        if (Input.GetMouseButton(0) && Situation == 5)//左クリックでRay発射
        {
            MiniGun();
        }
    }

    void HandGun()
    {
        if (HGcount >= 1)
        {
            Ray ray = new Ray(Barrel[0].transform.position, Player.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                bullet = Instantiate(bulletPrefab[0], Barrel[0].transform.position, Quaternion.identity);
                Vector3 worldDir = ray.direction;
                bullet.GetComponent<HandGunBulletScript>().Shot(worldDir * ballspeed);
                HGcount = 0.7f;
                Destroy(bullet, 1f);
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                bullet = Instantiate(bulletPrefab[0], Barrel[0].transform.position, Quaternion.identity);
                Vector3 worldDir = ray.direction;
                bullet.GetComponent<HandGunBulletScript>().Shot(worldDir * ballspeed);
                HGcount = 0.7f;
                Destroy(bullet, 1f);
            }
        }
    }

    void MachinePistol()
    {
        if (MPcount >= 1)
        {
            Ray ray = new Ray(Barrel[1].transform.position, Player.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                bullet = Instantiate(bulletPrefab[1], Barrel[1].transform.position, Quaternion.identity);
                Vector3 worldDir = ray.direction;
                bullet.GetComponent<MachinePistolBulletScript>().Shot(worldDir * ballspeed);
                MPcount = 0.85f;
                Destroy(bullet, 1f);
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                bullet = Instantiate(bulletPrefab[1], Barrel[1].transform.position, Quaternion.identity);
                Vector3 worldDir = ray.direction;
                bullet.GetComponent<MachinePistolBulletScript>().Shot(worldDir * ballspeed);
                MPcount = 0.85f;
                Destroy(bullet, 1f);
            }
        }
    }

    void AR()
    {
        if (ARcount >= 1)
        {
            Ray ray = new Ray(Barrel[2].transform.position, Player.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                bullet = Instantiate(bulletPrefab[2], Barrel[2].transform.position, Quaternion.identity);
                Vector3 worldDir = ray.direction;
                ARcount = 0.8f;
                bullet.GetComponent<ARBulletScript>().Shot(worldDir * ballspeed);

                Destroy(bullet, 1f);
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                bullet = Instantiate(bulletPrefab[2], Barrel[2].transform.position, Quaternion.identity);
                Vector3 worldDir = ray.direction;
                bullet.GetComponent<ARBulletScript>().Shot(worldDir * ballspeed);
                ARcount = 0.8f;
                Destroy(bullet, 1f);
            }
        }
    }

    void LMG()
    {
        if(LMGcount >= 1)
        {
            Ray ray = new Ray(Barrel[3].transform.position, Player.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                bullet = Instantiate(bulletPrefab[3], Barrel[3].transform.position, Quaternion.identity);
                Vector3 worldDir = ray.direction;
                bullet.GetComponent<LMGBulletScript>().Shot(worldDir * ballspeed);
                LMGcount = 0.8f;
                Destroy(bullet, 1f);
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                bullet = Instantiate(bulletPrefab[3], Barrel[3].transform.position, Quaternion.identity);
                Vector3 worldDir = ray.direction;
                bullet.GetComponent<LMGBulletScript>().Shot(worldDir * ballspeed);
                LMGcount = 0.8f;
                Destroy(bullet, 1f);
            }
        }
    }

    void MiniGun()
    {
        if (MNGcount >= 1)
        {
            Ray ray = new Ray(Barrel[4].transform.position, Player.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                bullet = Instantiate(bulletPrefab[4], Barrel[4].transform.position, Quaternion.identity);
                Vector3 worldDir = ray.direction;
                bullet.GetComponent<MiniGunBulletScript>().Shot(worldDir * ballspeed);
                MNGcount = 0.95f;
                Destroy(bullet, 1f);
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                bullet = Instantiate(bulletPrefab[4], Barrel[4].transform.position, Quaternion.identity);
                Vector3 worldDir = ray.direction;
                bullet.GetComponent<MiniGunBulletScript>().Shot(worldDir * ballspeed);
                MNGcount = 0.95f;
                Destroy(bullet, 1f);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "HandGun")
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
        }

        if (other.gameObject.name == "AR")
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
        }

        if (other.gameObject.name == "LMG")
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
        }

        if (other.gameObject.name == "MiniGun")
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
        }
    }
}
