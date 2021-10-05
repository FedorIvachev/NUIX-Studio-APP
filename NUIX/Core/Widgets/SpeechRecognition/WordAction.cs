using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WordAction : MonoBehaviour
{
    public string[] words;

    public UnityEvent wordRecognized;

    private void Start()
    {
        if (wordRecognized == null)
            wordRecognized = new UnityEvent();
    }
}
