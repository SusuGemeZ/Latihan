using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamaChara : MonoBehaviour
{
    public Character Sari;
    public Character Rina;
    // Start is called before the first frame update
    void Start()
    {
        Sari = CharacterManager.instance.GetCharacter ("Sari", enableCreateCharacterOnStart: false);
        Rina = CharacterManager.instance.GetCharacter ("Rina", enableCreateCharacterOnStart: false);
        
    }

    public string [] speech;
    int i = 0;
    
    public Vector2 moveTarget;
    public float moveSpeed;
    public bool smooth;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Space))
        {
            if (i < speech.Length)
                Sari.Say (speech[i]);
            else
                sapi.instance.Close ();

            i++;
        }

        if (Input.GetKey (KeyCode.M))
        {
            Sari.MoveTo (moveTarget, moveSpeed, smooth);
        }
        if (Input.GetKey (KeyCode.S))
        {
            Sari.StopMoving (true);
        }
        
    }
}
