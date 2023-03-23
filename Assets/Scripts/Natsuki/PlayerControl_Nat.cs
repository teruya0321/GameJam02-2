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
    public GameObject bulletPrefab1;

    GameObject bullet;

    public GameObject Barrel;
    public GameObject Barrel1;

    public float ballspeed;

    public GameObject Player;

    public float limit;
    public int maxlimit;
    public int minlimit;

    public Transform parentTran;
    public GameObject arm;


    void Start()
    {
        //InstantiatePrefab();

        controller = GetComponent<CharacterController>();

        roteuler = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);//視点移動の奴です

        limit = 50;
    }
    void Update()
    {

        Arm();

        limit += 10 * Time.deltaTime;

        limit = System.Math.Min(limit, maxlimit);
        limit = System.Math.Max(limit, minlimit);

        if (controller.isGrounded)
        {

            movedir.z = Input.GetAxisRaw("Vertical") * speed;


            movedir.x = Input.GetAxisRaw("Horizontal") * speed;

            if (Input.GetButton("Jump"))
            {
                movedir.y = 10f;
            }
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

    void Arm()
    {
        if (Input.GetMouseButtonDown(0))//左クリックでRay発射
        {
            LightArm();
        }

        if (Input.GetMouseButtonDown(1))//右クリックでRay発射
        {
            ReftArm();
        }
    }

    void LightArm()
    {
        limit -= 5;
        Ray ray = new Ray(Player.transform.position, Player.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);


            /*Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
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
            Destroy(bullet, 1f);*/
        }
    }

    void ReftArm()
    {
        limit -= 5;
        Ray ray = new Ray(Player.transform.position, Player.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);



            /* Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
             bullet = Instantiate(bulletPrefab1, Barrel1.transform.position, Quaternion.identity);
             Vector3 worldDir = ray.direction;
             bullet.GetComponent<BulletScript>().Shot(worldDir * ballspeed);
             Destroy(bullet, 1f);
     }
     else
     {
             Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
             bullet = Instantiate(bulletPrefab1, Barrel1.transform.position, Quaternion.identity);
             Vector3 worldDir = ray.direction;
             bullet.GetComponent<BulletScript>().Shot(worldDir * ballspeed);
             Destroy(bullet, 1f);*/
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Cube1")
        {
            //Vector3 pos = this.transform.position;
            other.transform.position = arm.transform.position;
            other.gameObject.transform.SetParent(parentTran);
            other.transform.localEulerAngles = Vector3.zero;
        }
    }
}
