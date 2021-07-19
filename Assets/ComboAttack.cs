using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboAttack : MonoBehaviour
{
    Animator animator;
    public Slider slider;
    public int sliderSpeed;
    public float minPos;
    public float maxpos;
    public RectTransform pass;
    public int combo = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void MakeCombo(int atkNum)
    {
        animator.SetFloat("Blend", atkNum);
        animator.SetTrigger("Atk");
    }
    bool isAtk = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAtk)
        {
            Debug.Log("Space 키 입력");
            SetAtk();
            isAtk = true;
        }
    }

    void SetAtk()
    {
        slider.value = 0;
        minPos = pass.anchoredPosition.x;
        maxpos = pass.sizeDelta.x + minPos;
        StartCoroutine(ComboAtk());
    }

    IEnumerator ComboAtk()
    {
        yield return null;
        while(!(Input.GetKeyDown(KeyCode.Space) || slider.value == slider.maxValue))
        {
            slider.value += Time.deltaTime * sliderSpeed;
            yield return null;
        }
        if(slider.value >= minPos && slider.value <= maxpos)
        {
            MakeCombo(combo++);

            if (combo < 3)
            {
                SetAtk();
            }
            else
            {
                combo = 0;
                isAtk = false;
            }
        }else
        {
            MakeCombo(0);
            combo = 0;
            isAtk = false;
        }
        slider.value = 0;
    }

}
