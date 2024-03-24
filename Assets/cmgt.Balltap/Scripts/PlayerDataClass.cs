using cmgt.Balltap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataClass : MonoBehaviour
{
    public string userId;
    public string name;
    public int currentScore;
    public int coins;
    public PlayerData playerData=new PlayerData();

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        playerData.userId ??= SystemInfo.deviceUniqueIdentifier;
        playerData.name ??= userId.ToString();
        SetPlayerData();
    }
    public void SetPlayerData()
    {
        playerData.currentScore = 10;
        playerData.coins = 10;
    }

}
