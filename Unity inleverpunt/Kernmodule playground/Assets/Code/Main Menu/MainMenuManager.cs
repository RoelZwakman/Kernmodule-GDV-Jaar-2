using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    public static MainMenuManager instance;
    private bool isLoadingLevel;

    void Awake()
    {
        instance = this;
    }

	public static void LoadLevel(int sceneToLoad, float timeBeforeLoad = 1)
    {
        instance.StartCoroutine(LoadScene(sceneToLoad, timeBeforeLoad));
    }

    public static void QuitGame(string yn = "n")
    {
        if (yn == "y" && instance.isLoadingLevel != true)
        {
            Debug.Log("MainMenuManager: Quitting game");
            Application.Quit();
        }

        else if (yn == "n")
        {
            Debug.Log("MainMenuManager: Quit game but yn was n. Returning...");
            return;
        }
    }

    static IEnumerator LoadScene(int sceneToLoad, float timeBeforeLoad)
    {
        instance.isLoadingLevel = true;
        yield return new WaitForSeconds(timeBeforeLoad);
        instance.isLoadingLevel = false;
        SceneManager.LoadScene(sceneToLoad);
    }
}
