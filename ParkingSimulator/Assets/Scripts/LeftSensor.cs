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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
            player.isLeft = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Car")
            player.isLeft = false;
    }
}
