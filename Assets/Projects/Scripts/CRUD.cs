using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CRUD : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_InputField roomInput;
    public Button postButton;
    private string BaseURL = "https://server-wistoria.vercel.app/campus";
    private string UpdateURL = "https://server-wistoria.vercel.app/campus/update";
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
        string name = nameInput.text;
        string room = roomInput.text;

        CreateItem(name, room);
    }

    public class Item
    {
        public string name;
        public string room;
    }

    public void CreateItem(string name, string room)
    {
        StartCoroutine(PostItem(new Item { name = name, room = room }));
    }
    
    IEnumerator PostItem(Item item)
    {
        string json = JsonUtility.ToJson(item);

        using (UnityWebRequest request = new UnityWebRequest(UpdateURL, "POST"))
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
