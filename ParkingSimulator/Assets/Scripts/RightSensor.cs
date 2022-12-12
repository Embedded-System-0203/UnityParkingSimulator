using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSensor : MonoBehaviour
{
    PlayerController player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Line")
            player.isRight = true;
        //Debug.Log("Right Line");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Line")
            player.isRight = false;
        //Debug.Log("Left Exit");
    }
}
