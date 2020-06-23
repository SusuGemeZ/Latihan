﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kuntul : MonoBehaviour
{
    sapi dialogue;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = sapi.instance;
        
    }

    public string [] s = new string[]
    {
        "Kamu, ngapain di sana?",
        "di sana bahaya loh..!!",
        "kamu bisa di kentu"

    };

    int index = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!dialogue.IsSpeaking || dialogue.IsWaitingForUserInput)
            {
                if (index >= s.Length)
                {
                    return;
                }
                Say(s[index]);
                index++;
            }
        }
    }
     void Say(string s)
    {
        string[] parts = s.Split(':');
        string speech = parts[0];
        string speaker = (parts.Length >= 2) ? parts[1] : "";

        dialogue.Say(speech, speaker);
    }
}