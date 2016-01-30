using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{


    void Awake()
    {
        SceneManager.LoadSceneAsync("GameUI", LoadSceneMode.Additive);
    }

    //    void Start()
    //    {
    //    }
    //
    //    void Update()
    //    {
    //    }

}
