using UnityEngine;
using System.Collections;

public class SampleUserPolling_ReadWrite : MonoBehaviour
{
    public SerialController serialController;

    PlayerController player;
    AbstractSerialThread serialThread;

    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        StartCoroutine(Send());
    }

    void Update()
    {
        //---------------------------------------------------------------------
        // Send data
        //---------------------------------------------------------------------



        player.isAccel = true;
        player.isBrake = false;

        //---------------------------------------------------------------------
        // Receive data
        //---------------------------------------------------------------------

        string message = serialController.ReadSerialMessage();

        if (message == null)
            return;

        


        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
            Debug.Log("Connection established");
        else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
            Debug.Log("Connection attempt failed or disconnection detected");
        else
            Debug.Log("Message arrived: " + message);



    }

    IEnumerator Send()
    {
        if (player.isLeft)  //자동차 왼쪽에 근접 장애물을 감지한 경우
        {
            Debug.Log("Sending Enter Left");
            serialController.SendSerialMessage("1");
        }
        else if(!player.isLeft) //자동차 왼쪽에 근접 장애물이 없는 경우
        {
            Debug.Log("Sending Exit Left");
            serialController.SendSerialMessage("2");
        }

        if (player.isRight)
        {
            Debug.Log("Sending Enter Right");
            serialController.SendSerialMessage("3");
        }
        else if (!player.isRight)
        {
            Debug.Log("Sending Exit Right");
            serialController.SendSerialMessage("4");
        }

        if (player.isBack)
        {
            Debug.Log("Sending Enter Back");
            serialController.SendSerialMessage("5");
        }
        else if (!player.isBack)
        {
            Debug.Log("Sending Exit Back");
            serialController.SendSerialMessage("6");
        }

        if (player.isCrash) //물체가 장애물에 충돌한 경우
        {
            Debug.Log("Sending Crash");
            serialController.SendSerialMessage("4");
        }

        yield return new WaitForSeconds(0.2f);

        StartCoroutine(Send());
    }
}
