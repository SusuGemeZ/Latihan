﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;
    public RectTransform characterPanel;
    public List<Character> characters = new List<Character>();
    public Dictionary<string, int> characterDictionary = new Dictionary<string, int>();

    void Awake()
    {
        instance = this;
    }
    public Character GetCharacter(string characterName, bool createCharacterIfDoesNotExist = true, bool enableCreateCharacterOnStart = true)
    {
        int index = -1;
        if (characterDictionary.TryGetValue(characterName, out index))
        {
            return characters [index];
        }
        else if (createCharacterIfDoesNotExist)
        {
            return CreateCharacter (characterName, enableCreateCharacterOnStart);
        }

        return null;
    }
    public Character CreateCharacter(string characterName, bool enableOnStart = true)
    {
        Character newCharacter = new Character(characterName, enableOnStart);

        characterDictionary.Add (characterName, characters.Count);
        characters.Add (newCharacter);

        return newCharacter;
    }
public class CHARACTERPOSITION
{
    public Vector2 bottomleft = new Vector2 (0, 0);
    public Vector2 topright = new Vector2 (1f, 1f);
    public Vector2 center = new Vector2 (0.5f, 0.5f);
    public Vector2 bottomright = new Vector2 (1f, 0);
}


}
