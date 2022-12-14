using UnityEngine;
using System.Collections;

public class SampleUserPolling_ReadWrite : MonoBehaviour
{
    public SerialController serialController;

    PlayerController player;
    AbstractSerialThread serialThread;

    bool preLeft = false;
    bool preRight = false;
    bool preBack = false;

    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        StartCoroutine(Send());
    }

    void Update()
    {
        string message = serialController.ReadSerialMessage();

        if (message == null)
            return;

        //split�� ����ϴ� ������ �߻��ϰ� �Ǵµ� �Ʒ� �ɼ��� �߰��� split���� ���� �߻��� ������ ������. 
        string[] m_list = message.Split('\n', System.StringSplitOptions.RemoveEmptyEntries);

        foreach (string s in m_list)
        {
            if (s == "B")
            {
                player.isAccel = false;
                player.isBrake = true;
            }
            else if (s == "G")
            {
                player.isAccel = true;
                player.isBrake = false;
            }
            else if (s == "D")
                player.gear = s.ToCharArray();  //char.Parse(s);
            else if (s == "R")
                player.gear = s.ToCharArray();
            else if (s == "P")
                player.gear = s.ToCharArray();
            else
                player.direction = double.Parse(s);
        }



    }

    IEnumerator Send()
    {
        if (player.isLeft)  //�ڵ��� ���ʿ� ���� ��ֹ��� ������ ���
        {
            if(preLeft != player.isLeft)
            {
                serialController.SendSerialMessage("1111");
                preLeft = player.isLeft;
            }
        }
        else if (!player.isLeft) //�ڵ��� ���ʿ� ���� ��ֹ��� ���� ���
        {
            if (preLeft != player.isLeft)
            {
                serialController.SendSerialMessage("22222222");
                preLeft = player.isLeft;
            }
        }

        if (player.isRight)
        {
            if (preRight != player.isRight)
            {
                serialController.SendSerialMessage("3333");
                preRight = player.isRight;
            }
        }
        else if (!player.isRight)
        {
            if (preRight != player.isRight)
            {
                serialController.SendSerialMessage("44444444");
                preRight = player.isRight;
            }
        }

        if (player.isBack)
        {
            if (preBack != player.isBack)
            {
                serialController.SendSerialMessage("5555");
                preBack = player.isBack;
            }
        }
        else if (!player.isBack)
        {
            if (preBack != player.isBack)
            {
                serialController.SendSerialMessage("66666666");
                preBack = player.isBack;
            }
        }

        if (player.isCrash) //��ü�� ��ֹ��� �浹�� ���
        {
            serialController.SendSerialMessage("7777");

            yield return new WaitForSeconds(0.1f);

            Time.timeScale = 0;
        }

        string temp = new string(player.gear);

        if (player.isEnd && temp == "P")
        {
            serialController.SendSerialMessage("77777777");

            yield return new WaitForSeconds(0.1f);

            Time.timeScale = 0;
        }

        yield return new WaitForSeconds(0.2f);

        StartCoroutine(Send());
    }
}
