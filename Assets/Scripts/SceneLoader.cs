using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    static string sceneToLoad;
    public static string SceneToLoad { get => sceneToLoad; }

    // Load
    public static void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Progress Load
    public static void ProgressLoad(string sceneName)
    {
        sceneToLoad = sceneName;
        SceneManager.LoadScene("LoadingProgress");
    }

    // Reload Level
    public static void ReloadLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        ProgressLoad(currentScene);
    }

    // Load Next Level
    public static void LoadNextLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        int nextLevel = int.Parse(currentSceneName.Split("Level")[1]) + 1;
        string nextSceneName = "Level" + nextLevel;

        if(SceneUtility.GetBuildIndexByScenePath(nextSceneName) == -1)
        {
            Debug.Log(nextSceneName + " does not exists");
            ProgressLoad("LevelProcedural");
            return;
        }

        ProgressLoad(nextSceneName);
    }
}
