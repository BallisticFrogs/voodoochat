using UnityEngine;
using System.Collections;

public class GhostTraversableObject : MonoBehaviour
{

    private Material solidMaterial;
    private Material traversableMaterial;

    private MeshRenderer meshRenderer;

    void Awake()
    {
        gameObject.layer = Layers.obstacles_noghost;
        meshRenderer = GetComponent<MeshRenderer>();
        solidMaterial = meshRenderer.material;
        traversableMaterial = (Material)Resources.Load("traversable");
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
}
