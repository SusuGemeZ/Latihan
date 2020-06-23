using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sapi : MonoBehaviour
{
    public static sapi instance;

    public ELEMENTS elements;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Say(string speech, string speaker = "")
    {
        StopSpeaking();
        
        speaking = StartCoroutine(Speaking(speech, false, speaker));

    }

    public void SayAdd(string speech, string speaker = "")
	{
		StopSpeaking();

		SpeechText.text = targetSpeech;

		speaking = StartCoroutine(Speaking(speech, true, speaker));
	}

    public void StopSpeaking()
    {
        if (IsSpeaking)
        {
            StopCoroutine(speaking);
        }
        speaking = null;
    }
    public bool IsSpeaking {get{return speaking != null;}}
    [HideInInspector] public bool IsWaitingForUserInput = false;

    public string targetSpeech = "";
    Coroutine speaking = null;
    IEnumerator Speaking(string speech, bool additive, string speaker = "")
    {
        panelspeech.SetActive(true);
        targetSpeech = speech;

        if (!additive)
            SpeechText.text = "";
        else
            targetSpeech = SpeechText.text + targetSpeech;

        SpeakerNameText.text = DetermineSpeaker(speaker); //temporary

        IsWaitingForUserInput = false;

        while(SpeechText.text !=targetSpeech)
        {
            SpeechText.text += targetSpeech[SpeechText.text.Length];
            yield return new WaitForEndOfFrame();
        }

        //text finish
        IsWaitingForUserInput = true;
        while(IsWaitingForUserInput)
          yield return new WaitForEndOfFrame();

        
        StopSpeaking();
    }

    string DetermineSpeaker(string s)
    {
        string retVal = SpeakerNameText.text; //Default return  is current name
        if (s != SpeakerNameText.text && s != "")
           retVal = (s.ToLower().Contains("narrator")) ? "" : s;

        return retVal;
    }

    public void Close()
    {
        StopSpeaking ();
        panelspeech.SetActive (false);
    }

    [System.Serializable]
    public class ELEMENTS
    {
        public GameObject panelspeech;
        public Text SpeechText;
        public Text SpeakerNameText;
    }
    public GameObject panelspeech {get{return elements.panelspeech;}}
    public Text SpeechText {get{return elements.SpeechText;}}
    public Text SpeakerNameText {get{return elements.SpeakerNameText;}}
}

