using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CRUD : MonoBehaviour
{
    public TMP_InputField addressInput;
    public TMP_InputField floorInput;
    public TMP_InputField roomInput;
    public Button postButton;
    private string BaseURL = "http://localhost:3000/campus"; 
    void Start()
    {
        Debug.Log("Hi");
        StartCoroutine(GetPrinter());
    }

    IEnumerator GetPrinter()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(BaseURL))
        {
            yield return request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }
        }
    }

    public void OnPostSubmitButtonClick()
    {
        string address = addressInput.text;
        string room = roomInput.text;
        string floor = floorInput.text;

        CreateItem(address, room, floor);
    }

    public class Item
    {
        public string address;
        public string room;
        public string floor;
    }

    public void CreateItem(string address, string room, string floor)
    {
        StartCoroutine(PostItem(new Item { address = address, room = room, floor = floor }));
    }
    
    IEnumerator PostItem(Item item)
    {
        string json = JsonUtility.ToJson(item);

        using (UnityWebRequest request = new UnityWebRequest(BaseURL, "POST"))
        {
            byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }
        }
    }
}
