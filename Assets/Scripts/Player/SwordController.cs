using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField]
    int m_damageAmount = 1;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damageAmount_);
            AudioManager.instance.Play("SwordHit");
            
            if (GameManager.s_isSpiderDead)
                return;

            GameManager.Instance.RemoveSpiderHealth(m_damageAmount);
            GameManager.Instance.AddXp(m_damageAmount);
        }
    }
}
