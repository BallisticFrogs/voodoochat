using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    private GhostTraversableObject[] traversableObjects;

    void Awake()
    {
        SceneManager.LoadSceneAsync("GameUI", LoadSceneMode.Additive);

        // find all traversable objects
        traversableObjects = GameObject.FindObjectsOfType<GhostTraversableObject>();
    }

    //    void Start()
    //    {
    //    }
    //
    //    void Update()
    //    {
    //    }

    public void ShowAllTraversableObjects(bool traversable)
    {
        foreach (GhostTraversableObject traversableObject in traversableObjects)
        {
            traversableObject.ShowTraversable(traversable);
        }
    }

}
