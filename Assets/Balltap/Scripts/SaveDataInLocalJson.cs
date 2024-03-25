using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataInLocalJson : MonoBehaviour
{
    public PlayerData PlayerData = new PlayerData();
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void SaveToJson()
    {
        string playerData=JsonUtility.ToJson(PlayerData);
        string filePath = Application.persistentDataPath + "/playerData.json";
        Debug.Log(filePath);

        System.IO.File.WriteAllText(filePath, playerData);
        Debug.Log("Data Saved");
    }
    public void LoadFromJson()
    {
        string filePath=Application.persistentDataPath + "/playerData.json";
        string playerData=File.ReadAllText(filePath);
        Debug.Log("Loaded From Json");
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
            SaveToJson();
        if(Input.GetKeyUp(KeyCode.L))
            LoadFromJson();
    }

}
[System.Serializable]
public class PlayerData
{
    public string userId;
    public string name;
    public int currentScore;
    public int coins;
}

