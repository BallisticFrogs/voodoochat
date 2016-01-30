using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour
{

    public ParticleSystem pickupVfx;

    private bool pickedUp;

    void OnTriggerEnter(Collider other)
    {
        if (!pickedUp && other.CompareTag(Tags.Player))
        {
            pickedUp = true;
            StartCoroutine(Pickup(other.gameObject));
        }
    }

    private IEnumerator Pickup(GameObject player)
    {
        // increment player counter
        CollectiblePicker picker = player.GetComponent<CollectiblePicker>();
        picker.PickedUpCollectible();

        // vfx
        pickupVfx.Play();
        yield return new WaitForSeconds(3);

        // destroy
        gameObject.DestroyRecursive();
    }

}
