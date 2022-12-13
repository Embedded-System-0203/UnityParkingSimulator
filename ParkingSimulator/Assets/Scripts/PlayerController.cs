using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public WheelCollider[] wheels = new WheelCollider[4];
    public GameObject[] sensors = new GameObject[3];

    public float power = 20f;
    public float rot = 45f;
    public float downForceValue;
    public float radius = 6f;
    public float direction; //���ӵ� ���� roll �� ���� ����

    public bool isLeft = false;
    public bool isRight = false;
    public bool isBack = false;
    public bool isCrash = false;

    public bool isAccel = false;    //����
    public bool isBrake = false;    //�극��ũ

    public char[] gear; //���

    GameObject[] sensorMesh = new GameObject[3];
    GameObject[] wheelMesh = new GameObject[4];
    Rigidbody rigid;


    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        rigid.centerOfMass = new Vector3(0, -1, 0);

        wheelMesh = GameObject.FindGameObjectsWithTag("WheelMesh");
        sensorMesh = GameObject.FindGameObjectsWithTag("SensorMesh");

        for(int i = 0; i < 4; i++)
        {
            wheels[i].transform.position = wheelMesh[i].transform.position;
        }
    }

    void FixedUpdate()
    {
        WheelPosAndRot();
        SensorPosAndRot();
        Move();
        AddDownForce();
        Brake();
    }

    void AddDownForce() //�ڵ��� ���Ÿ� ������ ����
    {
        rigid.AddForce(-transform.up * downForceValue * rigid.velocity.magnitude);
    }

    void Move()
    {
        string temp = new string(gear);

        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].motorTorque = Input.GetAxis("Vertical") * power;
        }

        for(int i = 0; i < 2; i++)
        {
            wheels[i].steerAngle = Input.GetAxis("Horizontal") * rot;
        }
    }

    void Brake()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            for(int i = 0; i < wheels.Length; i++)
            {
                wheels[i].brakeTorque = 100;
            }
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].brakeTorque = 0;
            }
        }
    }

    void WheelPosAndRot()
    {
        Vector3 wheelPosition = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;

        for (int i = 0; i < 4; i++)
        {
            wheels[i].GetWorldPose(out wheelPosition, out wheelRotation);
            wheelMesh[i].transform.position = wheelPosition;
            wheelMesh[i].transform.rotation = wheelRotation;
        }
    }

    void SensorPosAndRot()
    {
        for(int i = 0; i < 3; i++)
        {
            sensors[i].transform.position = sensorMesh[i].transform.position;
            sensors[i].transform.rotation = sensorMesh[i].transform.rotation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Car" || collision.transform.tag == "Wall")
        {
            isCrash = true;
        }
    }
}
