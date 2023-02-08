using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SavePoint : MonoBehaviour
{
    // Pelihahmon törmätessä tallennuspisteeseen, talletetaan tilatiedot JSON-tiedostoon
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DataManager.SavePlayerDataToJSON(other.GetComponent<Player>());
            Destroy(gameObject);
        }
    }
}