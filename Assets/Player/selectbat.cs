using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class selectbat : MonoBehaviour
{
    private System.Random _random = new System.Random();
    public int numberOfPlayers;
    public int[] players;
    public Text[] numberofText;
    public Button[] selectPlayerButton;
    private Button btn;
    public Transform BatImage;
    public Text BattingPositionNumber;
    public GameObject Header;
    public GameObject BattingNumberGameObject;
    public GameObject Bat;
    public GameObject start;
    public GameObject Reset;
    public GameObject SelectLine;
    public int ClickedNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GetNumber()
    {
        start.SetActive(false);
        Bat.SetActive(true);
        SelectLine.SetActive(true);
        Shuffle(players);
        for (var i = 0; i < numberOfPlayers; i++)
        {
            numberofText[i].text = players[i].ToString();
        }
    }

    public void GetNumbersInButton(int value)
    {
        Debug.Log(value);
        ClickedNumber = value;
        SelectLine.SetActive(false);
        BatImage.transform.DOLocalMoveY(250f, 1f);
        //numberofText[value].GetComponent<Text>().color = Color.green;
        BattingPositionNumber.text = value.ToString();
        Invoke(nameof(ShowResult), 1.1f);
        if (value==1)
        {
            Debug.Log("yes");
            //ShowResult();
        }
    }

    void ShowResult()
    {
        BattingNumberGameObject.SetActive(true);
        Header.SetActive(true);
        Reset.SetActive(true);
        for (var i = 0; i < numberOfPlayers; i++)
        {
            selectPlayerButton[i].onClick.RemoveAllListeners();
        }

       //btn.onClick.RemoveAllListeners();
    }
    void Shuffle(int[] array)
    {
        int p = array.Length;
        for (int n = p - 1; n >= 0; n--)
        {
            int r = _random.Next(0, n);
            int t = array[r];
            array[r] = array[n];
            array[n] = t;
            //Debug.Log(n);
            btn = selectPlayerButton[n].GetComponent<Button>();
            btn.onClick.AddListener(delegate { GetNumbersInButton(t); });
        }
    }

    public void RestAll()
    {
        start.SetActive(true);
        Bat.SetActive(false);
        Reset.SetActive(false);
        Header.SetActive(false);
        SelectLine.SetActive(true);
        BattingNumberGameObject.SetActive(false);
        BatImage.transform.DOLocalMoveY(20f, 0f);
       // numberofText[ClickedNumber].GetComponent<Text>().color = Color.white;


    }
        // Update is called once per frame
    void Update()
    {
        
    }
}
