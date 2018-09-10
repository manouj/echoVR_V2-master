using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

static class LoadScenes
{
    public static IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        async.allowSceneActivation = false;
        while (async.progress <= 0.89f)
        {
            //progressText.text = async.progress.ToString();
            yield return null;
        }
        async.allowSceneActivation = true;
    }
}
