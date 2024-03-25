using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Balltap
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public GameObject startbutton;
        public GameObject reStartButton;
        public bool isGameStart;
        // Start is called before the first frame update
        void Start()
        {
            Instance = this;
        }

        public void GameStart()
        {
            startbutton.SetActive(false);
            isGameStart = true;
        }
        public void RestartGame()
        {
            startbutton.SetActive(true);
            reStartButton.SetActive(false);
        }
        public void LoadHome()
        {
            SceneManager.LoadScene("SceneManager");
        }


    }
}
