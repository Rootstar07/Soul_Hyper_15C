using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brain : MonoBehaviour
{
    public int enemyHP;
    public int enemyShield;
    public float damage = 2;
    public GameManager gameManager;

    Animator animator;

    [Header("HP바")]
    public GameObject enemyHpBarPrefab;
    public GameObject canvas;
    public float hpHeight = 1f;
    public RectTransform hpBar;
    public Transform uiPos;
    public ShakeCamera shakeCamera;

    private void Start()
    {
        animator = GetComponent<Animator>();

        // HP바 생성
        hpBar = Instantiate(enemyHpBarPrefab, canvas.transform).GetComponent<RectTransform>();
        hpBar.transform.GetChild(0).GetComponent<Slider>().maxValue = enemyHP;
        hpBar.transform.GetChild(0).GetComponent<Slider>().value = enemyHP;
    }

    private void Update()
    {
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3
            (uiPos.position.x,
             uiPos.position.y, 
             uiPos.position.z));

        hpBar.position = _hpBarPos;
    }

    public void Attack()
    {

    }

    public void Parryed()
    {
        animator.SetBool("isParred", true);
    }



    public void ParryingStop()
    {
        animator.SetBool("isParred", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ParredCheck") && animator.GetBool("isParred"))
        {
            Debug.Log("패리됌 체크");
            other.transform.parent.gameObject.GetComponent<Animator>().SetBool("isParryAttack", true);

            // 기절 연장 필요           
            animator.SetTrigger("isParredAttacked");
            // 이때 두번 패링공격되지 않게 -> 스테마나? 쿨타임?
        }

        if (other.CompareTag("PlayerAttack"))
        {         
            if (gameManager.isParredAttack == true)
            {
                if (other.GetComponent<AttackBridge>().PlayerAttack() >= hpBar.transform.GetChild(0).GetComponent<Slider>().value)
                    gameManager.StartCoroutine("Pause", 0.5f);
                else
                    gameManager.StartCoroutine("Pause", 0.05f);
            }
            else
                gameManager.StartCoroutine("Pause", 0.05f);

            Debug.Log("적이 맞음");
            UpdateHp(other.GetComponent<AttackBridge>().PlayerAttack());
            shakeCamera.OnshakeCamera(0.15f, 0.1f);
        }
    }

    public void UpdateHp(float amount)
    {
        hpBar.transform.GetChild(0).GetComponent<Slider>().value -= amount;

        if (hpBar.transform.GetChild(0).GetComponent<Slider>().value <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        animator.SetBool("isDead", true);
    }

    public void Destroy()
    {
        Destroy(gameObject);
        Destroy(hpBar.gameObject);
    }

}
