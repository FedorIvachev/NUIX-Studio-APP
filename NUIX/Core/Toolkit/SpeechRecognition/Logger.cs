using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logger : MonoBehaviour
{

    public string output = "";
    public string stack = "";

    public int m_MaxLines = 6;
    private Queue<string> m_Inputs;

    private string wholeText;


    public void Awake()
    {
        m_Inputs = new Queue<string>();
    }


    // Start is called before the first frame update
    void Start()
    {
        Application.logMessageReceived += HandleLog;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddText(string newInput)
    {
        if (m_Inputs.Count >= m_MaxLines)
        {
            m_Inputs.Dequeue();
        }

        m_Inputs.Enqueue(newInput);

        UpdateText();
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        output = logString;
        stack = stackTrace;

        AddText(output);
    }

    public void UpdateText()
    {
        wholeText = "";

        foreach (string strDate in m_Inputs)
        {
            wholeText += (strDate + "\n");
        }

        GetComponent<Text>().text = wholeText;
    }
}
