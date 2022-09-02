using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;


[System.Serializable]
public class MyIntEvent : UnityEvent<string>
{
}

public class NUIXSpeechRecognition :  MonoBehaviour
{

    public WordAction[] trigger_words;

    //public UnityEvent<string> unityEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnalyzeSpeechRecognized(string result)
    {
        // Example: compare speech result to a set of words

        char[] separators = new char[] { ' ', '.' };

        foreach (var word in result.Split(separators, StringSplitOptions.RemoveEmptyEntries))
        {
            foreach (WordAction wordAction in trigger_words)
            {
                if (wordAction.words.Contains(word))
                {
                    wordAction.wordRecognized?.Invoke();
                }
            }
        }

    }
}
