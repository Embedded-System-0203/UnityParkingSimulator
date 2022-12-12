using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public WheelCollider[] wheels = new WheelCollider[4];
    public GameObject[] sensors = new GameObject[3];

    public float power = 100f;
    public float rot = 45f;
    public float downForceValue;
    public float radius = 6f;

    public bool isLeft = false;
    public bool isRight = false;
    public bool isBack = false;


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

    // Update is called once per frame
    void FixedUpdate()
    {
        WheelPosAndRot();
        SensorPosAndRot();
        Move();
        AddDownForce();
        //testInput();
    }

    void testInput()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            Debug.Log(Input.GetAxis("Vertical"));
        }
    }

    void AddDownForce() //�ڵ��� ���Ÿ� ������ ����
    {
        rigid.AddForce(-transform.up * downForceValue * rigid.velocity.magnitude);
    }

    void Move()
    {
        //if(Input.GetAxis("Horizontal") > 0)     //
        //{
        //    wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * Input.GetAxis("Horizontal"); //Input.GetAxis("Horizontal")�� �ٸ� ������ ����
        //    wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * Input.GetAxis("Horizontal");
        //}
        //else if(Input.GetAxis("Horizontal") < 0)    //
        //{
        //    wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * Input.GetAxis("Horizontal");
        //    wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * Input.GetAxis("Horizontal");
        //}


        for (int i = 0; i < wheels.Length; i++) // if(���==D && ���� ����)
        {
            wheels[i].motorTorque = Input.GetAxis("Vertical") * power;
        }

        for(int i = 0; i < 2; i++)
        {
            wheels[i].steerAngle = Input.GetAxis("Horizontal") * rot;
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

    private void OnTriggerStay(Collider other)
    {
        //if (other.tag == "Line")
            //Debug.Log("Line");
    }
}
