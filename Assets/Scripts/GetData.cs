using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class GetData : MonoBehaviour
{
    InputField outputArea;
    InputField inputArea;
    string inputText;

    // Start is called before the first frame update
    void Start()
    {
        outputArea = GameObject.Find("OutputArea").GetComponent<InputField>();
        inputArea = GameObject.Find("InputArea").GetComponent<InputField>();
        GameObject.Find("GetButton").GetComponent<Button>().onClick.AddListener(getData);
    }

    void getData()
    {
        StartCoroutine(GetData_Coroutine());
    }

    IEnumerator GetData_Coroutine()
    {
        outputArea.text = "Loading...";
        inputText = inputArea.text;

        //string URL = "https://www.openstreetmap.org/search?query=%E4%B8%96%E7%BA%AA%E5%A4%A7%E9%81%93#";
        //string URL = "https://www.openstreetmap.org/search?query=" + inputText;
        //string URL = "https://api.openstreetmap.org/api/0.6/notes.json?bbox=-0.65094,51.312159,0.374908,51.669148";
        string URL = "https://www.openstreetmap.org/api/0.6/node/602360462/ways";
        using (UnityWebRequest request = UnityWebRequest.Get(URL))
        {
            yield return request.SendWebRequest();
            outputArea.text = request.downloadHandler.text;
            Debug.Log(outputArea.text);
        }
    }


}
