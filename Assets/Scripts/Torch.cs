using UnityEngine;
using System.Collections;

public class Torch : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private Animator animator;

    #endregion

    #region Unity Methods

    public void Awake()
    {
        if (animator != null)
        {
            animator.SetInteger("Random", Random.Range(0, 4));
        }
    }

    #endregion
}