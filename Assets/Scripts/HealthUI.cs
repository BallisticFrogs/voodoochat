using UnityEngine;
using System.Collections;

public class HealthUI : MonoBehaviour
{

    public GameObject hearthFull1;
    public GameObject hearthFull2;
    public GameObject hearthFull3;
    public GameObject hearthEmpty1;
    public GameObject hearthEmpty2;
    public GameObject hearthEmpty3;

    void start()
    {
        hearthFull1.SetActive(true);
        hearthFull2.SetActive(true);
        hearthFull3.SetActive(true);
        hearthEmpty1.SetActive(false);
        hearthEmpty2.SetActive(false);
        hearthEmpty3.SetActive(false);
    }

    public void updateHeartUI(int health)
    {
        if (health >= 3)
        {
            hearthFull1.SetActive(true);
            hearthFull2.SetActive(true);
            hearthFull3.SetActive(true);
            hearthEmpty1.SetActive(false);
            hearthEmpty2.SetActive(false);
            hearthEmpty3.SetActive(false);
        } else if (health >= 2)
        {
            hearthFull1.SetActive(true);
            hearthFull2.SetActive(true);
            hearthFull3.SetActive(false);
            hearthEmpty1.SetActive(false);
            hearthEmpty2.SetActive(false);
            hearthEmpty3.SetActive(true);
        }
        else if (health >= 1)
        {
            hearthFull1.SetActive(true);
            hearthFull2.SetActive(false);
            hearthFull3.SetActive(false);
            hearthEmpty1.SetActive(false);
            hearthEmpty2.SetActive(true);
            hearthEmpty3.SetActive(true);
        }
        else 
        {
            hearthFull1.SetActive(false);
            hearthFull2.SetActive(false);
            hearthFull3.SetActive(false);
            hearthEmpty1.SetActive(true);
            hearthEmpty2.SetActive(true);
            hearthEmpty3.SetActive(true);
        }
    }
}
