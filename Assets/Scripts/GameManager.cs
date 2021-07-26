using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float hp;
    public float stemana;
    public float defensePower;
    public float attackDamage;
    public bool isParredAttack = false;
    public ShakeCamera shakeCamera;
    public Slider hpSlider;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        hpSlider.maxValue = hp;
        hpSlider.value = hpSlider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Pause(float p)
    {
        Time.timeScale = 0f;
        float pauseEndTime = Time.realtimeSinceStartup + p;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1;
    }

    public void changeDamage()
    {
        if (isParredAttack == true)
        {
            attackDamage = attackDamage * 2;
        }
        else
        {
            attackDamage = attackDamage * 0.5f;
        }
    }


    public void Damage(float damage, string mode)
    {
        if (mode == "Parry")
        {
            Debug.Log("패링 성공");
            shakeCamera.OnshakeCamera(0.15f, 0.1f);
            StartCoroutine("Pause", 0.15f);
        }
        else if (mode == "Defense")
        {
            Debug.Log("가드 성공");
            hpSlider.value = hpSlider.value - (damage / defensePower);
            shakeCamera.OnshakeCamera(0.15f, 0.05f);
            StartCoroutine("Pause", 0.1f);
        }
        else if (mode == "Hit")
        {
            Debug.Log("플레이어가 맞음");
            hpSlider.value = hpSlider.value - damage;
            shakeCamera.OnshakeCamera(0.15f, 0.05f);
            StartCoroutine("Pause", 0.05f);
        }
    }
}
