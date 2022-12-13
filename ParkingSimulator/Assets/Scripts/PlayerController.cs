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
    public float direction; //가속도 센서 roll 값 받을 변수

    public bool isLeft = false;
    public bool isRight = false;
    public bool isBack = false;
    public bool isCrash = false;

    public bool isAccel = false;    //엑셀
    public bool isBrake = false;    //브레이크

    public bool isEnd = false;

    public char[] gear; //기어

    GameObject[] sensorMesh = new GameObject[3];
    GameObject[] wheelMesh = new GameObject[4];
    Rigidbody rigid;

    public float accelNum;
    public float dirNum;


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
        Rotate();
        AddDownForce();
        Brake();
        GameEnd();
    }

    void AddDownForce() //자동차 흔들거림 방지를 위해
    {
        rigid.AddForce(-transform.up * downForceValue * rigid.velocity.magnitude);
    }

    void Move()
    {
        string temp = new string(gear);

        //전진 후진
        if (temp != "P" && isAccel)  //기어가 P가 아니고, 엑셀을 누르고 있는 상태면
            if (temp == "D") //기어가 D라면
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

        //앞바퀴 좌 우
        //for(int i = 0; i < 2; i++)
        //{
        //    wheels[i].steerAngle = Input.GetAxis("Horizontal") * rot;
        //}
    }

    void Rotate()
    {
        if (direction <= -5.0f)  //왼쪽
        {
            dirNum -= 0.05f;
            if (dirNum <= -1.0f)
                dirNum = -1.0f;
            for (int i = 0; i < 2; i++)
            {
                wheels[i].steerAngle = dirNum * rot;
            }
        }
        else if (direction >= 5.0f)
        {
            dirNum += 0.05f;
            if (dirNum >= 1.0f)
                dirNum = 1.0f;
            for (int i = 0; i < 2; i++)
            {
                wheels[i].steerAngle = dirNum * rot;
            }
        }
        else if (direction >= -5.0f && direction <= 5.0f)
        {
            dirNum = 0.0f;
            for (int i = 0; i < 2; i++)
            {
                wheels[i].steerAngle = dirNum * rot;
            }
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
        }
        else if(!isBrake)
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].brakeTorque = 0;
            }
        }

        //if(Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    for(int i = 0; i < wheels.Length; i++)
        //    {
        //        wheels[i].brakeTorque = 100;
        //    }
        //}
        //else if(Input.GetKeyUp(KeyCode.LeftShift))
        //{
        //    for (int i = 0; i < wheels.Length; i++)
        //    {
        //        wheels[i].brakeTorque = 0;
        //    }
        //}
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

    void GameEnd()
    {
        string temp = new string(gear);

        if (isEnd && temp == "P")
        {
            Debug.Log("Game End");
        }
    }
}
