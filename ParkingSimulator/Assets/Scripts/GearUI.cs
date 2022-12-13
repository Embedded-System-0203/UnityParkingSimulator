using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GearUI : MonoBehaviour
{
    PlayerController player;
    TextMeshProUGUI gear;
    string nowGear;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        gear = GameObject.Find("NowGear").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        nowGear = new string(player.gear);
        gear.text = nowGear;
    }
}
