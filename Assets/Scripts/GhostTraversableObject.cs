using UnityEngine;
using System.Collections;

public class GhostTraversableObject : MonoBehaviour
{

    private Material solidMaterial;
    private Material traversableMaterial;

    private MeshRenderer meshRenderer;

    private GameController gameController;

    void Awake()
    {
        gameObject.layer = Layers.obstacles_noghost;
        meshRenderer = GetComponent<MeshRenderer>();
        solidMaterial = meshRenderer.material;
        traversableMaterial = (Material)Resources.Load("traversable");

        gameController = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameController>();
        gameController.RegisterTraversable(this);
    }

    public void ShowTraversable(bool traversable)
    {
        if (traversable)
        {
            meshRenderer.material = traversableMaterial;
        }
        else
        {
            meshRenderer.material = solidMaterial;
        }
    }

    void OnDestroy()
    {
        gameController.RemoveTraversable(this);
    }

}
