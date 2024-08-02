using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Runtime.InteropServices;
//using Interhaptics.Platforms.Mobile;
//using Interhaptics

namespace Balltap
{
    public class BallManeger : MonoBehaviour
    {
        public static BallManeger instance;
        public GameObject ball;
        public GameObject bat;
        public Rigidbody rb;
        public float speed;
        [SerializeField] private float ineSpeed;
        [SerializeField] private Vector3 ballStartPosition;
        [SerializeField] private Vector3 batStartPosition;
        [SerializeField] private int ballTapCount;
        [SerializeField] private int TopScore;
        [SerializeField] private Text ballTapCountText;
        [SerializeField] private Text ballTapCountResultText;
        [SerializeField] private Text TopScoreText;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float batMinimumForce = 10;
        public Vector3 velocityChange;
        [SerializeField] private bool isBallEnter;
        [SerializeField] private GameObject[] sideBoxs;
        [SerializeField] private bool isFirstTime = false;

        [SerializeField] private Transform _targetPosition;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Transform _ui;
        Camera _camera;
        [SerializeField] private GameObject bounusStar;
        [SerializeField] private GameObject starPrefab;

        // Start is called before the first frame update
        private void Awake()
        {
            if(instance == null)
                instance = this;
        }
        void Start()
        {
            //Time.timeScale = 2f;
            ineSpeed = speed;
            rb.GetComponent<Rigidbody>();
            ballStartPosition = ball.transform.position;
            batStartPosition = bat.transform.position;
            BatDragging.instance._ballMbager = this;
            Physics.gravity = new Vector3(0, -60, 0);
            //vibration = new Interhaptics.Platforms.Mobile.MobileHapticsVibration();
            _camera = Camera.main;
            if (PlayerPrefs.HasKey(GameManager.Instance.plaeyrDataPlayerPrf))
            {
                TopScore = PlayerPrefs.GetInt(GameManager.Instance.plaeyrDataPlayerPrf, 0);
                Debug.Log(TopScore + "Saved Score");
            }
            
        }
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("bat"))
            {
                isFirstTime = true;
                if (BatDragging.instance.isBallTapping == false)
                {
                    if (speed > 1f && BatDragging.instance.batMovingSpeed == 0)
                    {
                        speed /= 2;
                        velocityChange.y = 0;
                        if (velocityChange.x >= 0)
                        {
                            velocityChange.x = (Vector3.right * 5).x;
                            rb.AddForce(5 * rb.mass * velocityChange * speed);
                        }
                        else if (velocityChange.x <= 0)
                        {
                            velocityChange.x = (Vector3.left * 5).x;
                            rb.AddForce(5 * rb.mass * velocityChange * speed);
                        }
                    }
                }
                else
                {
                    if (BatDragging.instance.isBallTapping == true)
                    {

                        speed = BatDragging.instance.batMovingSpeed;
                        if (BatDragging.instance.batMovingSpeed > maxSpeed)
                        {
                            speed = maxSpeed;
                            rb.velocity = Vector3.zero;
                            rb.angularVelocity = Vector3.zero;

                            velocityChange.x = (BatDragging.instance.getRotationOfBat() - 90) / -50;
                            rb.AddForce(100 * speed * rb.mass * velocityChange);

                        }
                        //if (GUI.Button(new Rect(0, 10, 100, 32), "Vibrate!"))
                        //Handheld.Vibrate();
                        Vibration.Vibrate(20);
                        //vibration.PlayVibration(0);
                        //this.onTap?.Invoke();
                        ballTapCount++;
                        ballTapCountText.text = ballTapCount.ToString();
                        Debug.Log(ballTapCount);
                    }
                }
            }
            else if (collision.gameObject.CompareTag("ground"))
            {
                StartCoroutine(ResetPostion());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("edge"))
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            else if (other.gameObject.CompareTag("edgeright"))
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            else if (BatDragging.instance.isBallStay == false && other.gameObject.CompareTag("edge"))
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.AddForce(velocityChange);
            }

            if (other.gameObject.CompareTag("star"))
            {
                Debug.Log("hitStar");
                Destroy(starPrefab);
            }

        }
        public void ApplyFakeForce()
        {
            rb.AddForce(velocityChange * rb.mass * 50);
        }
        void OnCollisionExit(Collision collider)
        {
            isBallEnter = true;
        }
        IEnumerator ResetPostion()
        {
            yield return new WaitForSeconds(0.2f);
            GameManager.Instance.reStartButton.SetActive(true);
            ballTapCountResultText.text = ballTapCount.ToString();
            
            if (ballTapCount > TopScore)
            {
                TopScore = ballTapCount;
                TopScoreText.text = TopScore.ToString();

                GameManager.Instance.topscore = TopScore;
                PlayerPrefs.SetInt(GameManager.Instance.plaeyrDataPlayerPrf, TopScore);
            }
            if (GameManager.Instance.isConnectedWithPlayServives)
            {
                Social.ReportScore(GameManager.Instance.topscore, GPGSIds.leaderboard_ball_tap, LeaderBoardUpdate);
            }
        }
        void LeaderBoardUpdate(bool success)
        {
            if(success) { Debug.Log("update LeaderBoard"); }
            else { Debug.Log("Unable to update leaderboard");}

        }

        IEnumerator Restart()
        {
            yield return new WaitForSeconds(0.2f);
            speed = ineSpeed;
            GameManager.Instance.reStartButton.SetActive(false);
            GameManager.Instance.startbutton.SetActive(true);
            ball.transform.position = ballStartPosition;
            bat.transform.position = batStartPosition;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            ballTapCount = 0;
            ballTapCountText.text = ballTapCount.ToString();
            GameManager.Instance.isGameStart = false;
            bat.GetComponent<BoxCollider>().enabled = true;
            velocityChange = new Vector3(0, 1.2f, 0);
            rb.useGravity = false;
            //SpawnStar();
        }
        public void ResetGame()
        {
            StartCoroutine(Restart());
        }
        void FixedUpdate()
        {
            Vector3 worldToScreen = _camera.WorldToScreenPoint(_targetPosition.position + _offset);
            worldToScreen.z = 0;
            worldToScreen.y = 2000;
            //Debug.Log(worldToScreen);
            if(_targetPosition.position.y>9)
            {
                _ui.gameObject.SetActive(true);
                _ui.position = worldToScreen;
            }
            else
            {
                _ui.gameObject.SetActive(false);
            }
        }
        public void SpawnStar()
        {
            Vector3 position = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(1f, 5.5f),5);
            starPrefab = Instantiate(bounusStar, position, Quaternion.identity);
        }
    }
}

