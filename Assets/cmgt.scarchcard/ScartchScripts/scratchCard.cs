using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace cmgt.scratchcard
{
    public class scratchCard : MonoBehaviour
    {
        public GameObject maskPrefab;
        private bool isPressed = false;
        [Range(0, 100)]
        public int maskChildCount;
        public GameObject scratchObj;
        private GameObject currectGameObject;
        RaycastHit hitinfo;
        Ray ray;
        public GameObject collectBtn;
        public GameObject cardCollider;
        public Vector3 lastMousePos;
        public GameObject rewardImage;
        public ScratchCardDatabase cards;
        public GameObject HomeBtn;
        public bool isReveled=false;


        private void Start()
        {
            ShowRewrdedCards();
        }
        public enum RewardType
        {
            Coins = 0,
            Gems = 1,
            Coustume = 2,
            Balls = 3,
            Bats = 4
        }
        // Update is called once per frame
        void Update()
        {
            if (isReveled)
                return;
            if (Input.GetMouseButtonDown(0))
            {

                Ray mousePose = Camera.main.ScreenPointToRay(Input.mousePosition);

                //Vector2 mousePos2D = new Vector2(mousePose.x, mousePose.y);

                var hit = Physics.Raycast(mousePose, out hitinfo, Mathf.Infinity);
                
                if (hit && hitinfo.collider.gameObject == cardCollider)
                {
                    currectGameObject = hitinfo.collider.gameObject;
                    Debug.Log(hitinfo.collider.gameObject.name);
                    isPressed = true;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isPressed = false;
            }
            ScratchCard();

        }
        public void ScratchCard()
        {
            if (!isPressed && !CardManager.instance.isCardReadyToScratch)
                return;
            var mousePos = Input.mousePosition;
            mousePos.z = 3;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);


            if (isPressed == true && scratchObj.activeSelf && scratchObj != null)
            {
                if (mousePos != lastMousePos)
                {
                    GameObject maskSprite = Instantiate(maskPrefab, mousePos, Quaternion.identity);
                    maskSprite.transform.parent = scratchObj.transform;
                    if (lastMousePos != null)
                    {
                        lastMousePos = mousePos;
                    }
                }


                if (scratchObj.transform.childCount > maskChildCount)
                {
                    RevelCard();
                }
            }
        }
        void RevelCard()
        {

            Destroy(this.scratchObj);
            isPressed = false;
            isReveled = true;
            StartCoroutine(getReward());
        }
        

        void ShowRewrdedCards()
        {
            int randomVal = Random.Range(0, cards.cardImages.Count);
            Debug.Log(randomVal);
            //Debug.Log((int)RewardType.Balls);
            rewardImage.GetComponent<SpriteRenderer>().sprite = cards.cardImages[randomVal];
            switch(randomVal)
            {
                case (int)RewardType.Coins:
                    Debug.Log("coins");
                    break;
                case (int)RewardType.Gems:
                    Debug.Log("Gems");
                    break;
                case (int)RewardType.Coustume:
                    Debug.Log("Coustume");
                    break;
                case (int)RewardType.Bats:
                    Debug.Log("Bats");
                    break;
                case (int)RewardType.Balls:
                    Debug.Log("Balls");
                    break;
                default:
                    Debug.Log("coins");
                    break;
            }
            

        }
        IEnumerator getReward()
        {
            //yield return new WaitForSeconds(0.5f);
            //ShowRewrdedCards();
            yield return new WaitForSeconds(0.5f);
            Sequence mySequence = DOTween.Sequence();
            mySequence.Insert(0, cardCollider.transform.DOScale(230f, 0.5f))
                .Insert(0.5f, cardCollider.transform.DOScale(200f, 0.5f));
            yield return new WaitForSeconds(0.1f);
            HomeBtn.SetActive(true);
        }
        public void ReturnHome()
        {
            SceneManager.LoadScene("SceneManager");
        }
    }
}
