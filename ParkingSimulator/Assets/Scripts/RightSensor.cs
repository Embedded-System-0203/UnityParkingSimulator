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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
            player.isRight = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Car")
            player.isRight = false;
    }
}
