using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using Balltap;

public class GooglePlayServiceManager : MonoBehaviour
{
    public Image GPSUserProfilePic;
    public Text GPSUserProfileName;
    public static GooglePlayServiceManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    void Start()
    {
        SignIn();
    }
    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // Continue with Play Games Services
            Debug.Log("Login Success");
            StartCoroutine(LocalImage());
            GPSUserProfileName.text = Social.localUser.userName;

            GameManager.Instance.isConnectedWithPlayServives=true;
        }
        else
        {
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            //PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
            Debug.Log("login Faild");
        }
    }

    IEnumerator LocalImage()
    {
        Texture2D texture;
        while (Social.localUser.image == null)
        {
            Debug.Log("Image not found");
            yield return null;  
        }
        Debug.Log("Image Found");
        texture=Social.localUser.image;
        GPSUserProfilePic.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0f, 0f));
    }
    public void ShowLeaderBoard()
    {
        Social.ShowLeaderboardUI();
    }

}
