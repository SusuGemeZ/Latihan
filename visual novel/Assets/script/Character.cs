using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character
{
    public string characterName;
    [HideInInspector]public RectTransform root;
    public bool isMultiLayerCharacter{get{ return renderers.renderer == null;}}
    public bool enable {get{ return root.gameObject.activeInHierarchy;} set{ root.gameObject.SetActive (value);}}    sapi dialogue;
    public Vector2 anchorPadding {get{ return root.anchorMax - root.anchorMin;}}
    public void Say(string speech, bool add = false)
    {
        if (!enable)
            enable = true;

        if (!add)
            dialogue.Say(speech, characterName);
        else
            dialogue.SayAdd(speech, characterName);
    }

    Vector2 targetPosition;
    Coroutine moving;
    bool isMoving{get{ return moving != null;}}
    public void MoveTo(Vector2 Target, float speed, bool smooth = true)
    {
        StopMoving ();
        moving = CharacterManager.instance.StartCoroutine(Moving(Target, speed, smooth));
    }

    public void StopMoving(bool berhentitepatditarget = false)
    {
        if (isMoving)
        {
            CharacterManager.instance.StopCoroutine(moving);
            if (berhentitepatditarget)
                SetPosition (targetPosition);
        }
        moving = null;

    }

    public void SetPosition(Vector2 target)
    {
        Vector2 padding = anchorPadding;

        float maxX = 1f - padding.x;
        float maxY = 1f - padding.y;

        Vector2 minAnchorTarget = new Vector2(maxX * targetPosition.x, maxY * targetPosition.y);
        while (root.anchorMin != minAnchorTarget)
        
            root.anchorMin = minAnchorTarget;
            root.anchorMax = root.anchorMin + padding;

    }
    IEnumerator Moving(Vector2 target, float speed, bool smooth)
    {
        targetPosition = target;

        Vector2 padding = anchorPadding;

        float maxX = 1f - padding.x;
        float maxY = 1f - padding.y;

        Vector2 minAnchorTarget = new Vector2(maxX * targetPosition.x, maxY * targetPosition.y);
        speed *= Time.deltaTime;
        //bergerak sampe target di tentukan
        while (root.anchorMin != minAnchorTarget)
        {
            root.anchorMin = (!smooth) ? Vector2.MoveTowards(root.anchorMin, minAnchorTarget, speed) : Vector2.Lerp (root.anchorMin, minAnchorTarget, speed);
            root.anchorMax = root.anchorMin + padding;
            yield return new WaitForEndOfFrame ();
        }

    }
    public Character (string _name, bool enableOnStart = true)
    {
        CharacterManager cm = CharacterManager.instance;
        GameObject prefab = Resources.Load("Character/character[" + _name + "]") as GameObject;
        GameObject ob = GameObject.Instantiate (prefab, cm.characterPanel);

        root = ob.GetComponent<RectTransform> ();
        characterName = _name; 

        renderers.renderer = ob.GetComponentInChildren<RawImage> ();
        if (isMultiLayerCharacter)
        {
            renderers.bodyRenderer = ob.transform.Find ("bodyLayer").GetComponent<Image> ();
            renderers.ekspresiRenderer = ob.transform.Find ("ekspresi").GetComponent<Image> ();
        }

        dialogue = sapi.instance;
        enable = enableOnStart;
    }

    [System.Serializable]
     public class Renderers
    {
        public RawImage renderer;
        public Image bodyRenderer;
        public Image ekspresiRenderer;
    }
    public Renderers renderers = new Renderers();
  

}
