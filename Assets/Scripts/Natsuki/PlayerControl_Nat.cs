using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl_Nat : MonoBehaviour
{
    CharacterController controller;
    Vector3 movedir = Vector3.zero;
    Vector3 roteuler;
    public float speed;

    public GameObject bulletPrefab;
    GameObject bullet;

    public GameObject Barrel;

    public float ballspeed;

    public GameObject Player;

    public int limit;
    public int maxlimit;
    public int minlimit;

    public Transform parentTran;
    public GameObject arm;

    public int count;

    int Situation = 0;//0=素手、1=武器あり

    void Start()
    {
        count = 0;

        controller = GetComponent<CharacterController>();

        roteuler = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);//視点移動の奴です

        limit = 0;
    }
    void Update()
    {
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
            LightArm();
        }
    }

    void LightArm()
    {
        limit -= 100;
        Ray ray = new Ray(Barrel.transform.position, Player.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
            bullet = Instantiate(bulletPrefab, Barrel.transform.position, Quaternion.identity);
            Vector3 worldDir = ray.direction;
            bullet.GetComponent<BulletScript>().Shot(worldDir * ballspeed);
            Destroy(bullet, 1f);
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
            bullet = Instantiate(bulletPrefab, Barrel.transform.position, Quaternion.identity);
            Vector3 worldDir = ray.direction;
            bullet.GetComponent<BulletScript>().Shot(worldDir * ballspeed);
            Destroy(bullet, 1f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Weapon1")
        {
            Situation = 1;
            other.transform.position = arm.transform.position;
            other.gameObject.transform.SetParent(parentTran);
            other.transform.localEulerAngles = Vector3.zero;
        }
    }

    public int Getlimit()
    {
        return limit;
    }
}
