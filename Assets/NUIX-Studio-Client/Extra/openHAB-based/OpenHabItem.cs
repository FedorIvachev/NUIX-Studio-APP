using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Tsinghua.HCI.IoThingsLab
{
    /// <summary>
    /// The main class for openHAB integration.
    /// </summary>
    public class OpenHabItem : MonoBehaviour
    {
        /// <summary>
        /// Perform a POST call to openHAB server by sending a command.
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="command"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public bool NetworkPOSTCall(string itemName, string command, out string response)
        {
            //var uwr = new UnityWebRequest("https://USER:PASSWORD@home.myopenhab.org/rest/items/" + itemName, "POST"); // Ask 费杰 to provide access to his server or to help to configure your own
            var uwr = new UnityWebRequest("http://localhost:8080/rest/items/" + itemName, "POST");
            byte[] jsonToSend = new UTF8Encoding().GetBytes(command);
            uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            uwr.SetRequestHeader("Content-Type", "text/plain");
            uwr.SetRequestHeader("Accept", "application/json");
            uwr.SendWebRequest();
            if (uwr.isNetworkError)
            {
                response = "Error While Sending: " + uwr.error;
                Debug.Log("Error While Sending: " + uwr.error);
                return false;
            }
            else
            {
                response = "Received: " + uwr.downloadHandler.text;
                Debug.Log("Received: " + uwr.downloadHandler.text);
                return true;
            }
        }
    }
}