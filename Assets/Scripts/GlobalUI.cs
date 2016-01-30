using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GlobalUI : MonoBehaviour
{

    public void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }

    public static void LoadLvl()
    {
        SceneManager.LoadScene("room1", LoadSceneMode.Single);
    }

}
