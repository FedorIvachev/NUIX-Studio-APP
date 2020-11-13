using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


namespace Tsinghua.HCI.IoTVRP
{
    public class OpenHabItem : MonoBehaviour
    {
        // Only POST Calls are supported, but other request types can be added, check OpenHAB REST API
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void NetworkPOSTCall(string itemName, string command)
        {
            // using localhost, so it can be performed only on the server at the moment
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

    }
}

