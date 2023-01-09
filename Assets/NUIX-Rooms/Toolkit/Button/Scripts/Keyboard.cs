using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keyboard : MonoBehaviour
{
    public TMP_Text inputField;
    public GameObject normalButtons;
    public GameObject capsButtons;

    private bool caps;
    // Start is called before the first frame update
    void Start()
    {
        caps = false;
    }

    public void InsertChar(string c)
    {
        inputField.text += c;
    }

    public void DeleteChar()
    {
        if (inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }

    public void InsertSpace()
    {
        inputField.text += " ";
    }

    public void CapsPressed()
    {
        normalButtons.SetActive(caps);
        capsButtons.SetActive(!caps);
        caps = !caps;
    }
}
