using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{


    public void ScratchCard()
    {
        SceneManager.LoadScene("ScratchCard");
    }
    public void BallTap()
    {
        SceneManager.LoadScene("Ball Tap");
    }

}
