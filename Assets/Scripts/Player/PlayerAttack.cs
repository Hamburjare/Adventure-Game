using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    Animator m_anim;

    private void Awake()
    {
        m_anim = GetComponent<Animator>();
    }

    public void OnMeleeAttack(InputValue value)
    {
        if (value.isPressed)
        {
            MeleeAttack();
        }
    }

    public void OnRangedAttack(InputValue value)
    {
        if (value.isPressed)
        {
            RangedAttack();
        }
    }

    public void RangedAttack()
    {
        m_anim.SetTrigger("Attacking2");
    }

    public void MeleeAttack()
    {
        m_anim.SetTrigger("Attacking1");
    }
}
