using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField] int m_damageAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damageAmount_);
            AudioManager.instance.Play("SwordHit");
            GameManager.Instance.RemoveSpiderHealth(m_damageAmount);
            GameManager.Instance.AddXp(m_damageAmount);
        }
    }
}
