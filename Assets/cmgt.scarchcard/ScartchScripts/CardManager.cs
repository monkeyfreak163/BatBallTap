using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace cmgt.scratchcard
{
    public class CardManager : MonoBehaviour
    {
        public GameObject flipingCards;
        public GameObject flipCardsText;
        public GameObject scratchCard;
        public static CardManager instance;
        public bool isCardReadyToScratch=false;
        // Start is called before the first frame update
        void Start()
        {
            instance = this;
        }

        public void CardsClicked()
        {
            StartCoroutine(ClickedCard());
        }
        IEnumerator ClickedCard()
        {
            flipCardsText.SetActive(false);
            Sequence mySeq = DOTween.Sequence();
            mySeq.Insert(0, flipingCards.transform.DOScale(0, 1f))
                .Insert(1f, scratchCard.transform.DOScale(1, 0.5f))
                .Insert(1.5f, scratchCard.transform.DOScale(0.8f, 0.2f))
                .Insert(1.7f, scratchCard.transform.DOScale(1, 0.1f));
            mySeq.SetAutoKill();
            Invoke("CardFlipDisable", 1.1f);
            yield return new WaitForSeconds(3f);
            isCardReadyToScratch = true;
        }
        void CardFlipDisable()
        {
            flipingCards.SetActive(false);
        }
    }
}
