using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSensor : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Line")
            Debug.Log("Left Line");
    }
}
