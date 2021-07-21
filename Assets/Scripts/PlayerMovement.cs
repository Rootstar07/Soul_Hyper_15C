using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("게임 매니저")]
    public GameManager gamemanager;

    [Header("플레이어 속도")]
    public float noramlSpeed;
    public float GuardSpeed;
    public float speed;

    [Space]
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
    //float sliderSpeed = 100;
    public bool comboStart = false;
    public int nowCombo = 0;

    public bool nowSlide = false;
    public bool canCombo = false;

    [Header("피격 변수")]
    public bool enough = false;

    [Header("패링 변수")]
    public bool canParing = false;

    [Header("공격과 패리 콜라이더 위치")]
    public GameObject attackCollider;
    public GameObject parryCollider;

    void Start()
    {
        MoveDir = Vector3.zero;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 데미지 상태가 아니라면 키 입력 가능
        if (animator.GetBool("isHit") == false)
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
                else if (animator.GetBool("isAttack0") == false
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

            if (Input.GetKeyDown(KeyCode.W))
            {

            }
            if (Input.GetKey(KeyCode.E))
            {
                animator.SetBool("isGuard", true);
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                animator.SetBool("isGuard", false);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {

            }

            if (animator.GetBool("isGuard") == true)
            {
                speed = GuardSpeed;
            }
            else if (canMove == true)
            {
                speed = noramlSpeed;
            }
            else
            {
                speed = 0;
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


    }


    private void FixedUpdate()
    {
        // 현재 캐릭터가 땅에 있고 공격상태가 아니라면
        if (controller.isGrounded && animator.GetBool("isHit") == false)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) //하나라도 움직인다.
            {
                // 위, 아래 움직임 셋팅. 
                MoveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));


                animator.SetFloat("_X", MoveDir.x);
                last_x = MoveDir.x;

                // ******************* 스프라이트 방향 정하는 메커니즘 ******************************************
                if (Input.GetAxisRaw("Horizontal") >= 0)
                {
                    PlayerSprite.flipX = false;

                    attackCollider.transform.position = new Vector3(
                        transform.position.x + 0.85f,
                        transform.position.y,
                        transform.position.z
                        );

                    parryCollider.transform.position = new Vector3(
                        transform.position.x + 0.85f,
                        transform.position.y,
                        transform.position.z
                        );
                }                 
                else
                {
                    PlayerSprite.flipX = true;

                    attackCollider.transform.position = new Vector3(
                    transform.position.x - 0.85f,
                    transform.position.y,
                    transform.position.z);

                    parryCollider.transform.position = new Vector3(
                        transform.position.x - 0.85f,
                        transform.position.y,
                        transform.position.z
                        );
                }                 

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
        if (animator.GetBool("isHit") == false)
        {
            controller.Move(MoveDir * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        // 이미 맞은 상태가 아니라면
        if (enough == false)
        {
            if (other.tag == "EnemyAttack")
            {
                if (animator.GetBool("isGuard"))
                {
                    if (canParing == true)
                    {
                        // 매니저에 정보전달
                        gamemanager.Damage(other.GetComponent<BrainBridge>().brain.damage, "Parry");

                        // 적의 모션 변경
                        other.GetComponent<BrainBridge>().brain.Parryed();
                    }
                    else
                    {
                        gamemanager.Damage(other.GetComponent<BrainBridge>().brain.damage, "Defense");
                    }
                }
                else
                {
                    gamemanager.Damage(other.GetComponent<BrainBridge>().brain.damage, "Hit");
                    animator.SetBool("isHit", true);
                    animator.SetBool("isDash", false);
                    nowSlide = false;
                }

            }
        }
    }
}
