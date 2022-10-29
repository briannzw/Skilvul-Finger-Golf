using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingProgress : MonoBehaviour
{
    public Image loadingImage;

    private void Start()
    {
        StartCoroutine(Progress());
    }

    IEnumerator Progress()
    {
        loadingImage.fillAmount = 0;

        yield return new WaitForSeconds(1);

        var asyncOp = SceneManager.LoadSceneAsync(SceneLoader.SceneToLoad);

        while (!asyncOp.isDone)
        {
            loadingImage.fillAmount = asyncOp.progress;
            yield return null;
        }
    }
}
