using UnityEngine;
using System.Collections;

public class CollectiblePicker : MonoBehaviour
{

    public int victoryRequirement = 3;

    private int collectibleCount = 0;

    public void PickedUpCollectible()
    {
        collectibleCount++;
        if (collectibleCount >= victoryRequirement)
        {
            GameController gameController = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameController>();
            gameController.Victory();
        }
    }

}
