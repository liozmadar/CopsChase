using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.SocialPlatforms;
using TMPro;
using UnityEngine.UI;

public class GPGSManager : MonoBehaviour
{
    PlayGamesClientConfiguration clientConfiguration;
    [SerializeField] TMP_Text googleButtonText;
    [SerializeField] Image googleButtonImage;
    [SerializeField] GameObject loader;

    // Start is called before the first frame update
    void Start()
    {
        ConfigureGPGS();
        if (!PlayGamesPlatform.Instance.IsAuthenticated()) SignInToGoogle();
        
    }

    internal void ConfigureGPGS()
    {
        clientConfiguration = new PlayGamesClientConfiguration.Builder().Build();
    }

    internal void SignIntoGPGS(SignInInteractivity interactivity, PlayGamesClientConfiguration configuration)
    {

        configuration = clientConfiguration;
        PlayGamesPlatform.InitializeInstance(configuration);
        PlayGamesPlatform.Activate();

        PlayGamesPlatform.Instance.Authenticate(interactivity, (code) =>
        {
           
        });
    }

    public void SignOutFromGoogle()
    {
        PlayGamesPlatform.Instance.SignOut();
    }

    public void SignInToGoogle()
    {
        SignIntoGPGS(SignInInteractivity.CanPromptAlways, clientConfiguration);

    }

    public void ToggleGoogleAuthorizetion()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated()) SignOutFromGoogle();
        else SignInToGoogle();
    }
}
