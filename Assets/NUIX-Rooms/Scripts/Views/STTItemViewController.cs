using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to a STTItemDescription, adding extra actions
/// </summary>
public class STTItemViewController : ItemViewController
{
    //private Dictionary<string, Action> phrases;
    //public event Action WhenSelected = delegate { };

    public SpeechRecognition speechRecognition;

    /// <summary>
    /// Compares the string to the list of sender method names (i.e. specified actions)
    /// </summary>
    /// <param name="result">Any string</param>
    public void AnalyzeSpeechRecognized(string result)
    {
        //char[] separators = new char[] { ' ', '.' };
        //foreach (var word in result.Split(separators, StringSplitOptions.RemoveEmptyEntries))
        foreach (KeyValuePair<string, ActionData> action in senderMethods)
        {
            if (result == action.Key)
            {
                CallReceiverMethod(result);
            }
        }
    }

    /// <summary>
    /// Get the last recognized string from the connected SpeechRecognition class 
    /// and add it to the list of sender methods
    /// </summary>
    public void AddPhraseFromRecognized()
    {
        AddPhrase(speechRecognition.RecognizedString());
    }

    private void AddPhrase(string phrase)
    {
        CreateNewOrUpdateExistingSenderMethod(new ActionData(itemID, phrase));
    }
}
