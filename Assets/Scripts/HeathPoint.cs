using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeathPoint : MonoBehaviour
{
    [SerializeField] int m_healthAmount = 1;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            GameManager.Instance.AddHeroHealth(m_healthAmount);
            Destroy(gameObject);
        }
    }
}
