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

    public GameObject[] Barrel;

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

    public int PSbulletpower;
    public int MPbulletpower;
    public int ARbulletpower;
    public int LMGFbulletpower;
    public int MGbulletpower;

    float PScount;
    float MPcount;
    float ARcount;
    float LMGcount;
    float MNGcount;


    public int hp = 0;

    public SkinnedMeshRenderer blendshapeRenderer;

    Animator anim;

    public int hpLowerTime = 2;
    float hpTimer;

    bool onCursor = true;
    void Start()
    {
        PScount = 0;
        MPcount = 0;
        ARcount = 0;
        LMGcount = 0;
        MNGcount = 0;

        count = 0;

        controller = GetComponent<CharacterController>();

        roteuler = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);//視点移動の奴です

        limit = 0;

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
        PScount += Time.deltaTime;
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
            //anim.SetBool("Idel", false);
            //anim.SetBool("Run", true);

            movedir.x = Input.GetAxisRaw("Horizontal") * speed;
            //anim.SetBool("Idel", false);
            //anim.SetBool("Run", true);

            if (Input.GetMouseButtonDown(1))//右クリックでジャンプ
            {
                movedir.y = 10f;
                anim.SetBool("Idel", false);
                anim.SetBool("Jump", true);
                Invoke("reSet", 0.5f);
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            count++;
            Invoke("Step", 0.3f);
            Invoke("reSet", 0.5f);
        }

        movedir.y -= 20f * Time.deltaTime;

        Vector3 globaldir = transform.TransformDirection(movedir);
        controller.Move(globaldir * Time.deltaTime);

        if (controller.isGrounded)
        {
            movedir.y = 0;
        }

        float mouseInputX = Input.GetAxis("Mouse X");//横の視点移動

        roteuler = new Vector3(0, roteuler.y + mouseInputX * 3, 0f);
        transform.localEulerAngles = roteuler;
    }

    void reSet()
    {
        anim.SetBool("Idel", true); 
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
            anim.SetBool("Idel", false);
            anim.SetBool("backStep", true);
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
            Pistol();
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

    void Pistol()
    {
        if (PScount >= 1)
        {
            Ray ray = new Ray(Barrel[0].transform.position, Player.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                    Debug.Log("あたりました");
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

    void MachinePistol()
    {
        if (MPcount >= 1)
        {
            Ray ray = new Ray(Barrel[1].transform.position, Player.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                    Debug.Log("あたりました");
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

    void AR()
    {
        if (ARcount >= 1)
        {
            Ray ray = new Ray(Barrel[2].transform.position, Player.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                    Debug.Log("あたりました");
                    ARcount = 0.8f;
                    /*bullet = Instantiate(bulletPrefab[2], Barrel[2].transform.position, Quaternion.identity);
                    Vector3 worldDir = ray.direction;
                    bullet.GetComponent<ARBulletScript>().Shot(worldDir * ballspeed);
                    Destroy(bullet, 1f); */
                }
                
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                ARcount = 0.8f;
                /*bullet = Instantiate(bulletPrefab[2], Barrel[2].transform.position, Quaternion.identity);
                Vector3 worldDir = ray.direction;
                bullet.GetComponent<ARBulletScript>().Shot(worldDir * ballspeed);
                Destroy(bullet, 1f);*/
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
                if (hit.collider.CompareTag("Enemy"))
                {
                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                    Debug.Log("あたりました");
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

    void MiniGun()
    {
        if (MNGcount >= 1)
        {
            Ray ray = new Ray(Barrel[4].transform.position, Player.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                    Debug.Log("あたりました");
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

        if (other.gameObject.name == "AssaultRifle")
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
