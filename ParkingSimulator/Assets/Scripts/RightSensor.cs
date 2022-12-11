using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSensor : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Line")
            Debug.Log("Right Line");
    }
}
