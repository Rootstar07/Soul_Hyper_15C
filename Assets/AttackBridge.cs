using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBridge : MonoBehaviour
{
    public GameManager gameManager;

    public float PlayerAttack()
    {
        return gameManager.attackDamage;
    }


}
