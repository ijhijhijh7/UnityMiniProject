using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float speed = 10f;

    [SerializeField] float jumpForce = 15f;

    [SerializeField] Collision coll;

    [SerializeField] float slideSpeed = 1.5f;

    private bool canMove = true;
    private bool wallGrab;
    private bool wallJumped;

    public int side = 1;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!canMove)
        {
            rb.velocity = Vector2.zero; // 현재 속도를 0으로 설정
            return;
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);

        Walk(dir);

        if (coll.onWall)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            else if (!coll.onGround)
            {
                WallSlide();
            }
        }


        if (Input.GetKeyDown(KeyCode.X))
        {
            Dash(xRaw, yRaw);
        }

        if (Input.GetKey(KeyCode.C))
        {
            if (coll.onGround)
                Jump(Vector2.up, false);
            if (coll.onWall && !coll.onGround)
            {
                WallJump();
            }
        }

        if (wallJumped && (coll.onGround || coll.onWall))
        {
            wallJumped = false; // 상태 초기화
            rb.velocity = new Vector2(rb.velocity.x, 0); // 수직 속도 초기화
        }
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
        {
            return;
        }
        if (!wallJumped)
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), .5f * Time.deltaTime);
        }
    }

    private void WallSlide()
    {
        if (!canMove) return;

        bool pushingWall = false;
        if ((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        // 수평 속도는 push, 수직 속도는 slideSpeed로 설정
        rb.velocity = new Vector2(push, -slideSpeed);
    }

    private void Jump(Vector2 dir, bool wall)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * jumpForce;
    }


    private void WallJump()
    {
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
        {
            side *= -1;
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / 1.5f + wallDir / 1.5f), true);

        wallJumped = true;
    }

    private IEnumerator DisableMovement(float time)
    {
        canMove = false; // 이동 불가 설정
        yield return new WaitForSeconds(time); // 지정한 시간 대기
        canMove = true; // 이동 가능 설정
    }

    private void Dash(float x, float y)
    {
        if (!canMove) return;

        StartCoroutine(PerformDash(x, y)); // 대시 수행 코루틴 호출
    }

    private IEnumerator PerformDash(float x, float y)
    {
        canMove = false; // 대시 중 이동 불가 설정
        rb.velocity = Vector2.zero; // 대시 시 기존 속도 초기화

        // 대시 방향과 속도 설정
        Vector2 dashDirection = new Vector2(x, y).normalized; // 대시 방향 계산
        float dashSpeed = 30f; // 대시 속도 설정

        // 대시 속도 적용
        rb.velocity = dashDirection * dashSpeed;

        // 대시 지속 시간
        yield return new WaitForSeconds(0.2f);

        canMove = true; // 대시 후 이동 가능 설정
    }

    public void StopMovement()
    {
        canMove = false;
    }
}