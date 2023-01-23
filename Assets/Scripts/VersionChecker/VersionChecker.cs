using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class VersionChecker : MonoBehaviour
{
    [SerializeField] string versionUrl;
    [SerializeField] string gameLinkInPlayStore;
    [SerializeField] string currentVersion;
    [SerializeField] string latestVersion;
    [SerializeField] GameObject versionPopupWindow;

    void Start()
    {
        if (Time.realtimeSinceStartup < 5)
        {
            StartCoroutine(GetTheLatestVersionFromTheURL());
        }
    }

    private IEnumerator GetTheLatestVersionFromTheURL()
    {
        UnityWebRequest loaded = new UnityWebRequest(versionUrl);
        loaded.downloadHandler = new DownloadHandlerBuffer();
        yield return loaded.SendWebRequest();
        latestVersion = loaded.downloadHandler.text;
        CheckVersion();
    }
    void CheckVersion()
    {
        int result = currentVersion.CompareTo(latestVersion);
        if(latestVersion != "" && result < 0)
        {
          //  MainSceneManager.Instance.HideAllWindows();
            versionPopupWindow.SetActive(true);
        }
    }

    public void ClickOnNotNowButton()
    {
        versionPopupWindow.SetActive(false);
      //  MainSceneManager.Instance.mainView.SetActive(true);
    }

    public void ClickOnUpdateButton()
    {
        Application.OpenURL(gameLinkInPlayStore);
    }


}
