using Google.Play.Review;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAppReviews : MonoBehaviour
{
    public static InAppReviews instance;
    private ReviewManager _reviewManager;
    PlayReviewInfo _playReviewInfo;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        if (Application.platform == RuntimePlatform.Android)
        {
            _reviewManager = new ReviewManager();
            //StartCoroutine(LaunchReview());
        }
    }
    public IEnumerator LaunchReview()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            var requestFlowOperation = _reviewManager.RequestReviewFlow();
            yield return requestFlowOperation;
            if (requestFlowOperation.Error != ReviewErrorCode.NoError)
            {
                // Log error. For example, using requestFlowOperation.Error.ToString().
                yield break;
            }
            _playReviewInfo = requestFlowOperation.GetResult();

            var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
            yield return launchFlowOperation;
            _playReviewInfo = null; // Reset the object
            if (launchFlowOperation.Error != ReviewErrorCode.NoError)
            {
                // Log error. For example, using requestFlowOperation.Error.ToString().
                yield break;
            }
            // The flow has finished. The API does not indicate whether the user
            // reviewed or not, or even whether the review dialog was shown. Thus, no
            // matter the result, we continue our app flow.
        }
    }
}
