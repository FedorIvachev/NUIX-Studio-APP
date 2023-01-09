using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPlateItemViewController : ItemViewController
{
    public GameObject keyboard;

    public TMPro.TMP_Text Plate;

    public string PlateText
    {
        get
        {
            return Plate.text; 
        }
        set
        {
            Plate.text = value;
        }
    }

    public bool IsKeyBoardActive
    {
        get
        {
            return gameObject.transform.Find("Visuals/Keyboard").gameObject.activeInHierarchy;
        }
        set
        {
            transform.Find("Visuals/Keyboard").gameObject.SetActive(value);
        }
    }

    private void Start()
    {

        receiverMethods.Add(nameof(ToggleEnableKeyboard));
    }

    public void ToggleEnableKeyboard()
    {
        if (keyboard != null)
        {
            keyboard.SetActive(!keyboard.activeInHierarchy);
        }
    }

    public void SetText (string plateText)
    {
        PlateText = plateText;
    }
}
