using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class BatManger : MonoBehaviour
{
    private System.Random _random = new System.Random();
    RaycastHit hit;
    public GameObject startMenu;
    public GameObject winMenu;
    public Text showNumber;
    public Material Material1;
    public Material whiteMat;
    public int[] players;
    public Vector3[] linesTransform;
    public GameObject[] lines;
    public GameObject[] numbers;
    public GameObject Bat;
    private GameObject hitObject;
    public GameObject currentLine;
    public GameObject selectLineText;

    public GameObject rewardCard;

    // Start is called before the first frame update
    void Start()
    {
        for(var i=0;i<=lines.Length-1;i++)
        {
            linesTransform[i] = lines[i].transform.localPosition;
            numbers[i].transform.DOScale(0, 1f);
        }
        //StartCoroutine(SuffleLine());
    }
    public void OpenGame()
    {
        startMenu.SetActive(false);
        StartCoroutine(SuffleLine());
    }
    public void ResetGame()
    {
        winMenu.SetActive(false);
        startMenu.SetActive(true);
        selectLineText.SetActive(true);
        Bat.transform.DOLocalRotate(new Vector3(0, 90, -90), 0f);
        rewardCard.transform.DOScale(0, 0f);
        currentLine.GetComponent<MeshRenderer>().material= whiteMat;
        for (var i = 0; i <= lines.Length - 1; i++)
        {
            lines[i].GetComponent<BoxCollider>().enabled = true;
            numbers[i].transform.DOScale(0, 0.5f);
        }

    }
    IEnumerator BatRotate()
    {
        selectLineText.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Bat.transform.DOLocalRotate(new Vector3(0, 0, -90), 1.5f).SetEase(Ease.InOutBack);

        if (hitObject != null)
        {
            currentLine = hitObject;
            currentLine.GetComponent<MeshRenderer>().material = Material1;
            string lineName = currentLine.name;
            string lineNumber = lineName.Substring(lineName.Length - 1);
            int numberLine = Convert.ToInt32(lineNumber);
            showNumber.text = numberLine.ToString();
            for (var i = 0; i <= lines.Length - 1; i++)
            {
                lines[i].GetComponent<BoxCollider>().enabled = false;
            }

            yield return new WaitForSeconds(1.5f);
            Sequence mySeq = DOTween.Sequence();
            mySeq.Insert(0, numbers[4].transform.DOScale(new Vector3(6.28f, 0.89f, 12.9f), 0.5f))
                .Insert(0.5f, numbers[3].transform.DOScale(new Vector3(6.28f, 0.89f, 12.9f), 0.5f))
                .Insert(1, numbers[2].transform.DOScale(new Vector3(6.28f, 0.89f, 12.9f), 0.5f))
                .Insert(1.5f, numbers[1].transform.DOScale(new Vector3(6.28f, 0.89f, 12.9f), 0.5f))
                .Insert(2, numbers[0].transform.DOScale(new Vector3(6.28f, 0.89f, 12.9f), 0.5f));

            mySeq.SetAutoKill();

            yield return new WaitForSeconds(2.5f);
            winMenu.SetActive(true);
            rewardCard.transform.DOScale(1, 1.2f).SetEase(Ease.OutBounce);
        }
    }

    IEnumerator SuffleLine()
    {
        yield return new WaitForSeconds(0f);
        Shuffle(linesTransform);
        for (var a = 0; a <= linesTransform.Length - 1; a++)
        {
            lines[a].transform.localPosition = linesTransform[a];
        }
    }
    // Update is called once per frame
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hitRay = Physics.Raycast(ray, out hit, Mathf.Infinity);
            if (hitRay)
            {
                hitObject = hit.collider.gameObject ;
                Debug.Log("object that was hit: " + hitObject.name);
                StartCoroutine(BatRotate());
            }
        }
    }
    
    void Shuffle(Vector3[] array)
    {
        int p = array.Length;
        for (int n = p - 1; n >= 0; n--)
        {
            int r = _random.Next(0, n);
            Vector3 t = array[r];
            array[r] = array[n];
            array[n] = t;
        }
    }
}
