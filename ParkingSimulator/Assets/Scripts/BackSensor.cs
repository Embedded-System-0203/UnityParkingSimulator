using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSensor : MonoBehaviour
{
    PlayerController player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Line" || other.tag == "Car")
            player.isBack = true;
        //Debug.Log("Back Line");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Line" || other.tag == "Car")
            player.isBack = false;
        //Debug.Log("Left Exit");
    }
}
