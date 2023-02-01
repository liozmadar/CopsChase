using Google.Play.AppUpdate;
using Google.Play.Common;
using System.Collections;
using UnityEngine;

public class InAppUpdatesManager : MonoBehaviour
{
    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            StartCoroutine(CheckForUpdate());
        }
    }
    private IEnumerator CheckForUpdate()
    {
        AppUpdateManager appUpdateManager = new AppUpdateManager();
        PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation =
          appUpdateManager.GetAppUpdateInfo();

        yield return appUpdateInfoOperation;

        if (appUpdateInfoOperation.IsSuccessful)
        {
            var appUpdateInfoResult = appUpdateInfoOperation.GetResult();
            var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();
            var startUpdateRequest = appUpdateManager.StartUpdate(
                  appUpdateInfoResult,
                  appUpdateOptions);
            yield return startUpdateRequest;
        }
        else
        {
            Debug.Log(appUpdateInfoOperation.Error);
            // Log appUpdateInfoOperation.Error.
        }
    }
}