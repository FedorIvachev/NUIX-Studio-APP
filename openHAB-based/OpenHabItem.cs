using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Tsinghua.HCI.IoThingsLab
{
    public class OpenHabItem : MonoBehaviour
    {
        public void LocalHostNetworkPOSTCall(string itemName, string command)
        {
            var uwr = new UnityWebRequest("http://localhost:8080/rest/items/" + itemName, "POST");
            byte[] jsonToSend = new UTF8Encoding().GetBytes(command);
            uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            uwr.SetRequestHeader("Content-Type", "text/plain");
            uwr.SetRequestHeader("Accept", "application/json");
            uwr.SendWebRequest();
            if (uwr.isNetworkError)
            {
                Debug.Log("Error While Sending: " + uwr.error);
            }
            else
            {
                Debug.Log("Received: " + uwr.downloadHandler.text);
            }
        }

        public void NetworkPOSTCall(string itemName, string command)
        {
            var uwr = new UnityWebRequest("https://USER:PASSWORD@home.myopenhab.org/rest/items/" + itemName, "POST"); // Ask 费杰 to provide access to his server or to help to configure your own
            byte[] jsonToSend = new UTF8Encoding().GetBytes(command);
            uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            uwr.SetRequestHeader("Content-Type", "text/plain");
            uwr.SetRequestHeader("Accept", "application/json");
            uwr.SendWebRequest();
            if (uwr.isNetworkError)
            {
                Debug.Log("Error While Sending: " + uwr.error);
            }
            else
            {
                Debug.Log("Received: " + uwr.downloadHandler.text);
            }
        }
    }
}