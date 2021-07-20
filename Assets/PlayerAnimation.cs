using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    PlayerMovement playerMovement;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = transform.parent.GetComponent<PlayerMovement>();
    }

    public void AttackStop()
    {
        playerMovement.nowCombo = 0;
        playerMovement.canMove = true;

        animator.SetBool("isAttack0", false);
        animator.SetBool("isAttack1", false);
        animator.SetBool("isAttack2", false);
        playerMovement.canCombo = false;
    }

    public void DashStop()
    {
        animator.SetBool("isDash", false);
        playerMovement.nowSlide = false;
    }

    public void SlideAttackStop()
    {
        animator.SetBool("isSlideAttack", false);
        animator.SetBool("isDash", false);
        playerMovement.nowSlide = false;
    }

    public void ComboStart()
    {
        playerMovement.canCombo = true;
    }

    public void ComboEnd()
    {
        playerMovement.canCombo = false;
    }

    public void HitStop()
    {
        animator.SetBool("isHit", false);
        playerMovement.enough = false;
    }

    public void paringStart()
    {
        playerMovement.canParling = true;
    }

    public void paringEnd()
    {
        playerMovement.canParling = false;
    }

}
