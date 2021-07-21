using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    public GameManager gameManager;
    PlayerMovement playerMovement;
    public bool isParryingAttack = false;

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
        playerMovement.canParing = true;
    }

    public void paringEnd()
    {
        playerMovement.canParing = false;
    }

    public void parryAttackEnd()
    {
        animator.SetBool("isParryAttack", false);
        animator.SetBool("isAttack0", false);
        animator.SetBool("isAttack1", false);
        animator.SetBool("isAttack2", false);

        playerMovement.canMove = true;
    }

    public void DamageUp()
    {
        gameManager.isParredAttack = true;
        gameManager.changeDamage();
    }

    public void DamageNormal()
    {
        gameManager.isParredAttack = false;
        gameManager.changeDamage();
    }

}
