using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace cmgt.scratchcard
{
    public class CardsFlip : MonoBehaviour
    {

        public List<Transform> cards;
        public List<float> Angle = new List<float>();
        public int NoOfCards;
        public int speed;
        public float requiedAngle;
        public float minAngle;
        public float maxAngle;
        public float averageAngle;
        public float currentAngle;
        Coroutine coroutine;

        public List<GameObject> pooldCards;
        public GameObject cardsToPool;
        public GameObject parentCard;
        public bool isOneCard;
        public float clickBtnWaitingTime;
        public bool isCardClicked=false;


        // Start is called before the first frame update
        void Start()
        {

            pooldCards = new List<GameObject>();

            GameObject tem;
            for (var a = 0; a <= NoOfCards; a++)
            {
                tem = Instantiate(cardsToPool);
                tem.transform.parent = parentCard.transform;

                pooldCards.Add(tem);

                cards.Add(pooldCards[a].GetComponent<RectTransform>());
                pooldCards[a].transform.localPosition = Vector3.zero;
                pooldCards[a].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                if (NoOfCards == 1)
                {
                    isOneCard = true;
                }

            }
            StartCoroutine(CardsMove());

        }

        IEnumerator CardsMove()
        {
            yield return new WaitForSeconds(0.5f);
            averageAngle = maxAngle - minAngle;
            float requiedAngle = averageAngle / NoOfCards;

            for (int i = 0; i <= NoOfCards; i++)
            {
                if (i == 0 && !isOneCard)
                {
                    currentAngle = minAngle;
                    Angle.Add(currentAngle);
                }
                else if (NoOfCards == 1 && isOneCard)
                {
                    currentAngle = 0;
                    Debug.Log($"the current angle is {currentAngle}");
                    Angle.Add(currentAngle);
                }
                else
                {
                    currentAngle += requiedAngle;
                    //Debug.Log($"the current angle is {currentAngle}");
                    Angle.Add(currentAngle);
                }
            }
            coroutine = StartCoroutine(StartRotating(0.1f));
        }
        IEnumerator StartRotating(float timeGap)
        {
            for (var i = 0; i <= NoOfCards; i++)
            {
                //Debug.Log(i);
                yield return new WaitForSecondsRealtime(timeGap);
                //Vector3 dir = new Vector3(0, 0, -Angle[i]);
                pooldCards[i].SetActive(true);
                cards[i].transform.Rotate(speed * Time.deltaTime * -Vector3.forward, Angle[i]);
                clickBtnWaitingTime = timeGap * NoOfCards-2; 
            }
            yield return new WaitForSecondsRealtime(clickBtnWaitingTime);
            isCardClicked = true;
        }
        public void ClickedCard()
        {
            if (isCardClicked)
            {
                CardManager.instance.CardsClicked();
            }

        }
    }
}
