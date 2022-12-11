using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSensor : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Line")
            Debug.Log("Back Line");
    }
}
