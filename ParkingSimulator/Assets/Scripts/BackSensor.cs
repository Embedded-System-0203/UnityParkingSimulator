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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Line" || other.tag == "Car")
            player.isBack = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Line" || other.tag == "Car")
            player.isBack = false;
    }
}
