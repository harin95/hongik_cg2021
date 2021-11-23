using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Work : MonoBehaviour
{
    //환경 설정
    public float speed = 1.0F;
    public float jumpSpeed = 0.001F;
    public float gravity = 0.001F;
    private Vector3 moveDirection = Vector3.zero;

    //플레이어와 유리발판 사이 인터렉션 위해 게임 오브젝트 변수 생성
    public GameObject glass;
    

    private void Start()
    {
        //태그로 깨지는 유리 찾음
        glass = GameObject.FindGameObjectWithTag("BreakGlass");
        
    }

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        Animator mAvartar = GetComponent<Animator>();

        if (controller.isGrounded)
        {
            //점프 기능 구현
            if (Input.GetKey(KeyCode.Space))
            {
                moveDirection.y = jumpSpeed;
                mAvartar.SetTrigger("Jump");
            }

            //쉬프트 점프 기능 구현
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    moveDirection.y = jumpSpeed * 2;
                    mAvartar.SetTrigger("Jump");
                }
            }

            //구슬 던지는 모션 구현
            if (Input.GetMouseButtonDown(0))
            {
                mAvartar.SetTrigger("Throw");
            }

            //방향키에 따르는 캐릭터의 모션 변경(W,A,D는 걷는 모션)
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                mAvartar.SetBool("MoveAction0", true);

            }
            else
            {
                mAvartar.SetBool("MoveAction0", false);

            }
            //S는 뒤로 걷는 모션
            if (Input.GetKey(KeyCode.S))
            {
                mAvartar.SetBool("BackMotion", true);
            }
            else
            {
                mAvartar.SetBool("BackMotion", false);
            }

        }

        //중력으로 캐릭터가 y축을 따라 내려오도록 구현
        moveDirection.y -= gravity * Time.deltaTime * 2;
        controller.Move(moveDirection * Time.deltaTime / 20);

        float move_side = 0f;
        float move_forth = 0f;

        //방향키에 따르는 캐릭터의 이동거리 구현
        if (Input.GetKey(KeyCode.W))
        {
            move_forth += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move_forth -= 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move_side -= 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move_side += 1f;
        }
        //R키를 누를 시 처음 자리로 부활
        if (Input.GetKey(KeyCode.R))
        {
            transform.position = new Vector3(0, 0, 0);
        }
        transform.Translate(new Vector3(move_side, 0f, move_forth) * Time.deltaTime * 7);

    }

    //플레이어와 충돌 물체 처리
    void OnControllerColliderHit(ControllerColliderHit hit)
    {

        /*
         * 보완필요한 부분:
         * 1. 리스폰 -> 깨지는유리를 밟으면 안깨짐. (처음 밟고 떨어지는 유리는 잘깨짐)
           2. 구슬을 던진 유리에 점프하면 유리가 안 깨짐.       
           결론 -> 충돌이 한번 일어나면 다음 충돌이 안 일어남.
         */


        //플레이어가 깨지는 유리발판을 밟는 경우
        if (hit.collider.CompareTag("BreakGlass"))
        {
            Debug.Log("플레이어가 일반 유리 밟음");
            glass.GetComponent<BreakableWindow>().breakWindow();
 
        }
        //플레이어가 바닥에 떨어졌을 시 처음 자리로 부활
        if (hit.collider.CompareTag("deadFloor"))
        {
            Debug.Log("플레이어가 deadFloor 밟음");
            transform.position = new Vector3(0, 0.05f, 0);
        }

    }
    
}