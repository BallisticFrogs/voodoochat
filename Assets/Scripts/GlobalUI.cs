using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GlobalUI : MonoBehaviour
{

    public static int actualGameScene = 1;

    public void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }

    public static void LoadLvl()
    {
        SceneManager.LoadScene(actualGameScene, LoadSceneMode.Single);
    }

    public void LoadGame()
    {
        LoadLvl();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
