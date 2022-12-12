using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSensor : MonoBehaviour
{
    PlayerController player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Line")
            player.isLeft = true;
            //Debug.Log("Left Line");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Line")
            player.isLeft = false;
        //Debug.Log("Left Exit");
    }
}
