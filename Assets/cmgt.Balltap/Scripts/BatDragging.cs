using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace cmgt.Balltap
{
    public class BatDragging : MonoBehaviour
    {

        Vector3 v3;
        private float distance;
        public float batMovingSpeed;

        public Vector3 batStartPos;
        private bool dragging = true;
        private Vector3 offset;
        private Transform toDrag;
        public Rigidbody rb;
        public float Xmin, Xmax;
        public float Ymin, Ymax;
        public static BatDragging instance;
        public float minRotAngle;
        public float maxRotAngle;
        public float RotSpeed;

        public float startRottion = -100;
        public float batStartPosition2 = -100;
        public bool isBallStay = false;
        public BallManeger _ballMbager;
        public float batLastMovingSpeed;
        public bool isBallTapping = false;

        public GameObject startInstructionTxt;
        private void Awake()
        {
            batStartPosition2 = gameObject.transform.position.x;
        }
        private void Start()
        {
            instance = this;
            rb.GetComponent<Rigidbody>();
        }
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("ball"))
            {
                isBallStay = true;
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("ball"))
            {
                isBallStay = false;
            }
        }
        // Update is called once per frame
        void Update()
        {
            BatDragControll();
        }
        void BatDragControll()
        {
            if (Input.touchCount != 1)
            {
                dragging = false;
                return;
            }
            Touch touch = Input.touches[0];
            Vector3 pos = touch.position;
            if (touch.phase == TouchPhase.Began && GameManager.Instance.isGameStart)
            {
                Ray ray = Camera.main.ScreenPointToRay(pos);
                RaycastHit hit;
                batStartPos = new Vector3(0, this.transform.position.y, 0);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("bat"))
                    {
                        toDrag = hit.transform;
                        distance = hit.transform.position.z - Camera.main.transform.position.z;
                        v3 = new Vector3(pos.x, pos.y, distance);
                        v3 = Camera.main.ScreenToWorldPoint(v3);
                        offset = toDrag.position - v3;
                        dragging = true;
                        //rb.AddForce(Vector3.up);
                    }
                }
            }

            if (dragging  &&  touch.phase == TouchPhase.Moved)
            {
                startInstructionTxt.SetActive(false);
                _ballMbager.rb.useGravity = true;
                _ballMbager.velocityChange.y = 1.2f;
                v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
                v3 = Camera.main.ScreenToWorldPoint(v3);
                v3.x = Mathf.Clamp(v3.x, Xmin, Xmax);
                v3.y = Mathf.Clamp(v3.y, Ymin, Ymax);

                // toDrag.position = v3 + offset;
                toDrag.position = Vector3.MoveTowards(toDrag.position, v3 + offset, 30 * Time.deltaTime);

                batMovingSpeed = Vector3.Distance(new Vector3(0, transform.position.y, 0), batStartPos) * 100;
                isBallTapping = true;
                //Debug.Log("bat move speed"+batMovingSpeed);
                if (isBallStay)
                {
                    if (transform.position.y > batStartPos.y)
                    {
                        _ballMbager.ApplyFakeForce();
                        _ballMbager.velocityChange.x = Random.Range(-1f, 1f);
                    }
                }
                //  batStartPos = new Vector3(0, this.transform.position.y, 0);
            }
            if (dragging && touch.phase == TouchPhase.Stationary)
            {
                //Debug.Log("isBallTapping");
                isBallTapping = false;
                batMovingSpeed = _ballMbager.speed;
                //Debug.Log(batMovingSpeed);
                //_ballMbager.velocityChange.y = 0;
                //
            }
            if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
            {

                dragging = false;
                isBallTapping = false;
                batMovingSpeed = 0;
                this.gameObject.transform.DOMove(batStartPos, 0.5f).SetEase(Ease.OutBounce);
                this.gameObject.transform.DORotate(new Vector3(-10.36f, 0, 90), 0.5f);

            }

            if (startRottion != -100 && batStartPosition2 != -100)
            {
                transform.rotation = Quaternion.Euler(-10.36f, 0, getRotationOfBat());
            }
        }
        public float getRotationOfBat()
        {
            return (startRottion + ((maxRotAngle * (transform.position.x - batStartPosition2) / Xmax)));
        }
    }
}
