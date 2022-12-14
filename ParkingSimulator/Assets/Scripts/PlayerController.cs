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
    public double direction; //���ӵ� ���� roll �� ���� ����

    public bool isLeft = false;
    public bool isRight = false;
    public bool isBack = false;
    public bool isCrash = false;

    public bool isAccel = false;    //����
    public bool isBrake = false;    //�극��ũ

    public bool isEnd = false;

    public char[] gear; //���

    public float accelNum;
    public float dirNum;

    GameObject[] sensorMesh = new GameObject[3];
    GameObject[] wheelMesh = new GameObject[4];
    Rigidbody rigid;

    string startGear = "P";


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

        gear = startGear.ToCharArray();
    }

    void FixedUpdate()
    {
        WheelPosAndRot();
        SensorPosAndRot();
        Move();
        Rotate();
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

        //���� ����
        if (temp != "P" && isAccel)  //�� P�� �ƴϰ�, ������ ������ �ִ� ���¸�
            if (temp == "D") //�� D���
            {
                accelNum += 0.05f;
                if (accelNum >= 1)
                    accelNum = 1.0f;
                for (int i = 0; i < wheels.Length; i++)
                {
                    wheels[i].motorTorque = accelNum * power;
                }
            }
            else if(temp == "R")
            {
                accelNum -= 0.05f;
                if (accelNum <= -1)
                    accelNum = -1.0f;
                for (int i = 0; i < wheels.Length; i++)
                {
                    wheels[i].motorTorque = accelNum * power;
                }
            }
    }

    void Rotate()
    {
        if (direction <= -10.0f)  //����
        {
            dirNum -= 0.05f;
            if (dirNum <= -1.0f)
                dirNum = -1.0f;
            //for (int i = 0; i < 2; i++)
            //{
            //    wheels[i].steerAngle = dirNum * rot;
            //}
            wheels[1].steerAngle = dirNum * rot;    //�����ϸ� ������ 0,1�� �ƴ� 1,3�� �չ����� �Ǿ �̷��� ������
            wheels[3].steerAngle = dirNum * rot;    //�����ϸ� ������ 0,1�� �ƴ� 1,3�� �չ����� �Ǿ �̷��� ������
        }
        else if (direction >= 10.0f)
        {
            dirNum += 0.05f;
            if (dirNum >= 1.0f)
                dirNum = 1.0f;
            //for (int i = 0; i < 2; i++)
            //{
            //    wheels[i].steerAngle = dirNum * rot;
            //}
            wheels[1].steerAngle = dirNum * rot;
            wheels[3].steerAngle = dirNum * rot;
        }
        else if (direction >= -10.0f && direction <= 10.0f)
        {
            if (dirNum < 0)
                dirNum += 0.05f;
            else if (dirNum > 0)
                dirNum -= 0.05f;

            //dirNum = 0.0f;
            //for (int i = 0; i < 2; i++)
            //{
            //    wheels[i].steerAngle = dirNum * rot;
            //}
            wheels[1].steerAngle = dirNum * rot;
            wheels[3].steerAngle = dirNum * rot;
        }
    }

    void Brake()
    {
        if(isBrake)
        {
            accelNum = 0.0f;
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].brakeTorque = 100;
            }
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = accelNum * power;
            }
        }
        else if(!isBrake)
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ParkingEndLine")
        {
            isEnd = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ParkingEndLine")
        {
            isEnd = false;
        }
    }
}
