using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonController : MonoBehaviour
{
    public float poisonSpeed;

    [SerializeField]
    int m_damageAmount = 1;

    private void Update()
    {
        transform.Translate(0, 0, poisonSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // AudioManager.instance.Play("PoisonHit");
            GameManager.Instance.RemoveHeroHealth(m_damageAmount);
        }
    }
}
