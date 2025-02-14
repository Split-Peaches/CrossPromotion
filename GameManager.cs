using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    private string jsonURL = "https://yourserver.com/games.json"; // Replace with your URL
    private string localJsonPath;

    void Start()
    {
        localJsonPath = Path.Combine(Application.persistentDataPath, "games.json");

        if (File.Exists(localJsonPath))
        {
            LoadLocalData();
        }
        else
        {
            StartCoroutine(DownloadJSON());
        }
    }

    IEnumerator DownloadJSON()
    {
        UnityWebRequest request = UnityWebRequest.Get(jsonURL);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonData = request.downloadHandler.text;
            File.WriteAllText(localJsonPath, jsonData); // Save locally
            Debug.Log("JSON data saved locally!");
            ParseJSON(jsonData);
        }
        else
        {
            Debug.LogError("Failed to download JSON: " + request.error);
        }
    }

    void LoadLocalData()
    {
        string jsonData = File.ReadAllText(localJsonPath);
        Debug.Log("Loaded JSON from local storage.");
        ParseJSON(jsonData);
    }

    void ParseJSON(string jsonData)
    {
        GameData gameData = JsonUtility.FromJson<GameData>(jsonData);
        
        foreach (GameInfo game in gameData.games)
        {
            Debug.Log($"Title: {game.title}, Download: {game.download}");
        }
    }
}
