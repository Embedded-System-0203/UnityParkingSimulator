using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndUI : MonoBehaviour
{
    public GameObject tip;
    public GameObject crash;
    public GameObject end;

    PlayerController player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        tip.SetActive(false);
        crash.SetActive(false);
        end.SetActive(false);
    }

    void Update()
    {
        string temp = new string(player.gear);

        if(player.isCrash)
        {
            crash.SetActive(true);
        }

        if(player.isEnd)
        {
            tip.SetActive(true);
        }

        if (player.isEnd && temp == "P")
        {
            end.SetActive(true);
        }
    }
}
