using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManger : MonoBehaviour
{
    public GameObject StartButton;
    public GameObject LeaderBoardBtn;
    public GameObject PlayerName;
    public GameObject playerInputPanel;
    public GameObject LeaderBoardPanel;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("UserName"))
        {
            ActivateMainMenu();
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public void ActivateMainMenu()
    {
        StartButton.SetActive(true);
        LeaderBoardBtn.SetActive(true);
        PlayerName.GetComponent<TextMeshProUGUI>().enabled = true;
        playerInputPanel.SetActive(false);
    }
    public void EnableLeaderBoardPanel()
    {
        LeaderBoardPanel.SetActive(true);
    }
    public void DisableLeaderBoardPanle()
    {
        LeaderBoardPanel.SetActive(false);
    }
}
