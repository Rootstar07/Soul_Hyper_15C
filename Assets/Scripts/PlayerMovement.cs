using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float dashSpeed;
    public float gravity;

    private CharacterController controller; // 현재 캐릭터가 가지고있는 캐릭터 컨트롤러 콜라이더.
    private Vector3 MoveDir;                // 캐릭터의 움직이는 방향.
    public Animator animator;
    float last_x;
    float last_z;
    public bool canMove = true;
    public SpriteRenderer PlayerSprite;

    [Header("슬라이딩 콤보어택용")]
    public Slider slider;
    public float minPos;
    public float maxPos;
    public RectTransform pass;
    float sliderSpeed = 100;
    public bool comboStart = false;
    public int nowCombo = 0;

    public bool nowSlide = false;
    public bool canCombo = false;

    void Start()
    {
        MoveDir = Vector3.zero;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {

        // Q를 눌렀을때
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 대쉬 상태라면
            if (animator.GetBool("isDash") == true)
            {
                animator.SetBool("isSlideAttack", true);
                // 예외처리
                animator.SetBool("isAttack0", false);
                animator.SetBool("isAttack1", false);
                animator.SetBool("isAttack2", false);
            }
            // 처음 공격 상태라면
            else if(animator.GetBool("isAttack0") == false
                    && animator.GetBool("isAttack1") == false 
                    && animator.GetBool("isAttack2") == false)
            {
                animator.SetBool("isAttack0", true);
                nowCombo = 0;
                canMove = false;
            }
            // 콤보 공격중이라면
            else if (animator.GetBool("isAttack0") == true
                    || animator.GetBool("isAttack1") == true
                    || animator.GetBool("isAttack2") == true)
            {
                // 콤보영역에 들어옴
                if (canCombo == true)
                {
                    canCombo = false;
                    nowCombo++;
                    if (nowCombo == 1)
                    {
                        animator.SetBool("isAttack1", true);
                    }
                    else if (nowCombo == 2)
                    {
                        animator.SetBool("isAttack2", true);
                    }
                }
                else
                {

                }
            }
        }

        //******************************* 슬라이드식 콤보 어택 **********************************************************

        /*
        void SetAtk()
        {
            slider.value = 0;
            minPos = pass.anchoredPosition.x;
            maxPos = pass.sizeDelta.x + minPos;

            if (nowCombo <= 1)
            {
                nowCombo++;
                StartCoroutine(ComboAtk());
            }
        }

        IEnumerator ComboAtk()
        {
            yield return null;
            while (!(Input.GetKeyDown(KeyCode.Q) || slider.value == slider.maxValue))
            {
                slider.value += Time.deltaTime * sliderSpeed;
                yield return null;
            }

            if (slider.value >= comboMin && slider.value <= comboMax)
            {
                
                if (nowCombo == 1)
                {
                    Debug.Log("판정 성공1");
                    animator.SetBool("isAttack1", true);
                }
                else if (nowCombo == 2)
                {
                    Debug.Log("판정 성공2");
                    animator.SetBool("isAttack2", true);
                    // 돌진 넣기
                }
                SetAtk();
            }
            else
            {
                Debug.Log("판정실패");
                slider.value = 0;
                nowCombo = 0;
            }


        }
        */



        if (Input.GetKeyDown(KeyCode.W))
        {

        }
        if (Input.GetKeyDown(KeyCode.E))
        {

        }
        if (Input.GetKeyDown(KeyCode.R))
        {

        }

        if (canMove == false)
        {
            speed = 0;
        }
        else
        {
            speed = 5;
        }


    }


    private void FixedUpdate()
    {
        // 현재 캐릭터가 땅에 있는가?
        if (controller.isGrounded)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) //하나라도 움직인다.
            {
                // 위, 아래 움직임 셋팅. 
                MoveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));


                animator.SetFloat("_X", MoveDir.x);
                last_x = MoveDir.x;

                if (last_x > 0)
                    PlayerSprite.flipX = false;
                else
                    PlayerSprite.flipX = true;

                animator.SetFloat("_Y", MoveDir.z);
                last_z = MoveDir.z;

                animator.SetBool("IsMoving", true);
            }
            else
            {
                MoveDir = new Vector3(0, 0, 0);

                animator.SetBool("IsMoving", false);
                animator.SetFloat("_X", last_x);
                animator.SetFloat("_Y", last_z);
            }

            // 벡터를 로컬 좌표계 기준에서 월드 좌표계 기준으로 변환한다.
            MoveDir = transform.TransformDirection(MoveDir);

            // 스피드 증가.
            MoveDir *= speed;

            // 캐릭터 대쉬
            if (Input.GetButton("Jump") && nowSlide == false)
            {
                //슬라이드 중복 방지
                nowSlide = true;

                // 대쉬 이동
                MoveDir.x = dashSpeed * last_x;
                MoveDir.z = dashSpeed * last_z;
                animator.SetBool("isDash", true);

                // 공격하고 있다면 초기화
                comboStart = false;
                canMove = true;
                nowCombo = 0;
                animator.SetInteger("Combo", 0);

                // 슬라이드 시작
                //StartCoroutine(ComboAtk());
            }

            // 캐릭터 점프
            //if (Input.GetButton("Jump"))
            //    MoveDir.y = jumpSpeed;
        }

        // 캐릭터에 중력 적용.
        MoveDir.y -= gravity * Time.deltaTime;

        // 캐릭터 움직임.
        controller.Move(MoveDir * Time.deltaTime);
    }



    
    }
