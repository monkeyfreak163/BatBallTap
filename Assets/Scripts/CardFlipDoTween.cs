using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class CardFlipDoTween : MonoBehaviour
{
    public RectTransform[] cards;
    public GameObject[] cardsGameObject;
    public float waitingTime = 2f;
    public float animationTime = 0.2f;
    public Vector3[] cardPos;
    public RectTransform FirstCard;
    public Vector3 angle;
    private bool isCardTouched;
    public float[] cardRotation;
    public int numberOfCards;
    // Start is called before the first frame update
    void Start()
    {
        //CardMove();
        GetPosition();
        StartCoroutine(CardAnimation());
    }
    void GetPosition()
    {
        for (var a = 0; a < cardPos.Length; a++)
        {
            cardPos[a] = cards[a].transform.position;
            cardRotation[a] = cards[a].localEulerAngles.z;
        }
    }
    IEnumerator CardAnimation()
    {
        yield return new WaitForSeconds(0.001f);
        
        for (var b = 0; b <= cardPos.Length-1; b++)
        {
            cardsGameObject[b].SetActive(true);
            cards[b].transform.localPosition = FirstCard.transform.localPosition;
            cards[b].transform.localEulerAngles = FirstCard.localEulerAngles;
            Debug.Log(cards[0].transform.position);
        }
        yield return new WaitForSeconds(1f);
        Debug.Log("Sequnce");
        Sequence mySequence = DOTween.Sequence();
        mySequence.Insert(0, cards[0].transform.DOMove(cardPos[0], animationTime))
            .Insert(0,cards[0].transform.DORotate(new Vector3(0, 0, cardRotation[0]), animationTime, RotateMode.Fast))
            .Insert(0.2f, cards[1].transform.DOMove(cardPos[1], animationTime))
            .Insert(0.2f, cards[1].transform.DORotate(new Vector3(0, 0, cardRotation[1]), animationTime, RotateMode.Fast))
            .Insert(0.4f, cards[2].transform.DOMove(cardPos[2], animationTime))
            .Insert(0.4f, cards[2].transform.DORotate(new Vector3(0, 0, cardRotation[2]), animationTime, RotateMode.Fast))
            .Insert(0.6f, cards[3].transform.DOMove(cardPos[3], animationTime))
            .Insert(0.6f, cards[3].transform.DORotate(new Vector3(0, 0, cardRotation[3]), animationTime, RotateMode.Fast))
            .Insert(0.8f, cards[4].transform.DOMove(cardPos[4], animationTime))
            .Insert(0.8f, cards[4].transform.DORotate(new Vector3(0, 0, cardRotation[4]), animationTime, RotateMode.Fast))
            .Insert(1f, cards[5].transform.DOMove(cardPos[5], animationTime))
            .Insert(1, cards[5].transform.DORotate(new Vector3(0, 0, cardRotation[5]), animationTime, RotateMode.Fast))
            .Insert(1.2f, cards[6].transform.DOMove(cardPos[6], animationTime))
            .Insert(1.2f, cards[6].transform.DORotate(new Vector3(0, 0, cardRotation[6]), animationTime, RotateMode.Fast))

            ;
        yield return new WaitForSeconds(1f);
        isCardTouched = true;

    }

    void CalculateAngles()
    {

    }

    void MoveMyCards() {
        
    }

    void ClickedCard()
    {
        if(isCardTouched)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("yes");
                //CardManager.instance.CardsClicked();
            }
        }
        
    }
    void CardMove()
    {
        angle = new Vector3(Mathf.Cos(90), Mathf.Sin(90), 0);
        cards[0].transform.localPosition = angle;
    }
    // Update is called once per frame
    void Update()
    {
        ClickedCard();
    }
}
