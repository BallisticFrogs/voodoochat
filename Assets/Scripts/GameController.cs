using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public delegate void UILoaderHandler();
    public event UILoaderHandler OnUiLoaded;

    private GhostTraversableObject[] traversableObjects;

    void Awake()
    {
        StartCoroutine(LoadUIAsync());

        // find all traversable objects
        traversableObjects = GameObject.FindObjectsOfType<GhostTraversableObject>();
    }

    private IEnumerator LoadUIAsync()
    {
        // load async and wait
        yield return SceneManager.LoadSceneAsync("GameUI", LoadSceneMode.Additive);

        // execute callbacks
        if (OnUiLoaded != null) OnUiLoaded.Invoke();
    }

    public void ShowAllTraversableObjects(bool traversable)
    {
        foreach (GhostTraversableObject traversableObject in traversableObjects)
        {
            traversableObject.ShowTraversable(traversable);
        }
    }

}
