using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public float slowModeSpeed = 0.5f;

    public delegate void UILoaderHandler();
    public event UILoaderHandler OnUiLoaded;

    private readonly List<GhostTraversableObject> traversableObjects = new List<GhostTraversableObject>();
    private readonly List<Slowable> slowableObjects = new List<Slowable>();

    void Awake()
    {
        StartCoroutine(LoadUIAsync());
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

    public void SetWorldSpeed(float speed)
    {
        foreach (Slowable slowableObject in slowableObjects)
        {
            slowableObject.SetWorldSpeed(speed);
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RegisterSlowable(Slowable slowable)
    {
        slowableObjects.Add(slowable);
    }

    public void RemoveSlowable(Slowable slowable)
    {
        slowableObjects.Remove(slowable);
    }

    public void RegisterTraversable(GhostTraversableObject traversable)
    {
        traversableObjects.Add(traversable);
    }

    public void RemoveTraversable(GhostTraversableObject traversable)
    {
        traversableObjects.Remove(traversable);
    }

}
