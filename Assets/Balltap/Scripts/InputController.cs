using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    public Button submitButton;
    public TMP_InputField inputField;
    public TextMeshProUGUI playerName;
    private MenuManger menuManager;

    private string[] randomNames =
    {
        "jeff","max","sam","john","raj","vax"
    };
    // Start is called before the first frame update
    void Start()
    {
        menuManager = FindObjectOfType<MenuManger>();
        if(!PlayerPrefs.HasKey("UserName"))
        {
            PlayerPrefs.SetString("UserName", randomNames[Random.Range(0,randomNames.Length)]);
        }
        playerName.text = PlayerPrefs.GetString("UserName");
        submitButton.onClick.AddListener(()=> SaveName());
        submitButton.onClick.AddListener(()=> menuManager.ActivateMainMenu());
    }
    void SaveName()
    {
        if(inputField.text != "" && inputField.text.Length <=6)
        {
            PlayerPrefs.SetString("UserName", inputField.text);
            playerName.text = PlayerPrefs.GetString("UserName");
            inputField.text = "";
        }
        else
        {
            inputField.text = "Error";
        }
    }
}
