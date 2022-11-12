using System;
using System.Collections;
using Cinemachine;
using DG.Tweening;
using Game.Scripts;
using Game.Scripts.Event;
using QFramework;
using UnityEngine;
public enum PlayerState
{
    Idle,
    Run,
    Jump,
    Die,
    Dash
}
[Serializable]
public class PlayParameter
{
    //------------行走速度--------------
    public float speed;

    public bool isRuning = false;

    //-------------跳跃------------------
    //跳跃力度
    public float jumpForce;
    //跳跃次数
    public int jumpTimes = 1;

    public bool isJump;

    //--------------冲刺-----------------
    //冲刺中
    public bool isDashing;

    //冲刺时间
    public float dashduringTime;

    //冲刺力度
    public float dashForce;

    //CD相关
    public bool canDash = true;
    public float dashCD;

    public float dashStartTime;
    public bool productDashShaow;

    //------------攀爬--------------
    public float climbSlideSpeed;
    public float climbSlideMaxSpeed;
    public bool isClibing;
    public bool isClibingJump;
    public bool isClibingJumpOver;


    //-------面向----------------
    public bool faceRight;

    //-----------场景交互---------------
    public bool isInGround;
    public bool GroundTouch;
    public bool isFalling;
    public bool isinSky;
    public bool isinLeftWall;
    public bool isinRightWall;
    public float checkGoundRadius;
    public Vector2 checkGoundOffset;
    public float checkLeftWallRadius;
    public Vector2 checkLeftWallOffset;
    public float checkRightWallRadius;
    public Vector2 checkRightWallOffset;
    
    //粒子------------------------------
    public ParticleSystem RunDot;
    public ParticleSystem JumpDot;
    public Vector3 JumpV3;
    
    //地形检测---------------------
    public bool grassGd;
    public bool snowGd;

    //组件
    public Animator ar;
    public Rigidbody2D rb;
    public SpriteRenderer sp;
    public Transform ts;
    public BetterJump BetterJump;
    public GameObject Trail;
}

public class PlayerMoveCtr : MonoBehaviour,IController
{
    public PlayParameter P;

    public Vector2 velocity;
    

    // Start is called before the first frame update
    void Start()
    {
        TypeEventSystem.Global.Register<CloseTrailEvt>(CloseTrail).UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        velocity = P.rb.velocity;
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        P.BetterJump.enabled = !P.isInGround;
        DashCoolDown();
        if (Input.GetButtonDown("Dash") && !P.isDashing)
        {
            Dash();
        }
        Run();
        if (P.isinLeftWall)
        {
            Climb(-1);
        }else if(P.isinRightWall) Climb(1);

        GroundTouchCheck();
        PlayerStateManager();
        SceneStateManger();
        PlayerAniManager();
    }
    private void FixedUpdate()
    {
        DashShadow();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("GrassGd"))
        {
            P.grassGd = true;
            P.snowGd = false;
        }else if (col.gameObject.CompareTag("SnowGd"))
        {
            P.grassGd = false;
            P.snowGd = true;
        }
    }

    void GroundTouchCheck()
    {
        if (P.isInGround && !P.GroundTouch)
        {
            P.JumpDot.Play();
            P.GroundTouch = true;
           
        }

        if (P.isinSky || P.isinLeftWall || P.isinRightWall)
        {
            P.GroundTouch = false;
        }
    }

    void CloseTrail(CloseTrailEvt closeTrailEvt)
    {
        StartCoroutine(CloseTrail1());
    }

    IEnumerator CloseTrail1()
    {
        P.Trail.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        P.Trail.SetActive(true);
    }

    void Run()
    {
        if (P.isDashing || P.isClibingJump)return;
        float speed = Input.GetAxis("Horizontal") * P.speed;
        P.rb.velocity = new Vector2(speed, P.rb.velocity.y);
        PlayRunDot();
        if (!P.isClibingJumpOver)
        {
            P.rb.velocity = new Vector2(speed, P.rb.velocity.y);
        }
        else
        {
            P.rb.velocity = Vector2.Lerp(P.rb.velocity, new Vector2(Input.GetAxis("Horizontal") * P.speed, P.rb.velocity.y), 10 * Time.deltaTime);
        }

        if (Mathf.Abs(Input.GetAxis("Horizontal")) >0.1f && P.isInGround)
        {
            TypeEventSystem.Global.Send(new PlayerWalkEvt
            {
                isGrass = P.grassGd
            });
        }
        
    }

    private void PlayRunDot()
    {
        if (P.isInGround && !P.RunDot.isPlaying && P.RunDot.isStopped && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f )
        {
            P.RunDot.Play();
            
        }

        if ((!P.isInGround && P.RunDot.isPlaying && !P.RunDot.isStopped) || Mathf.Abs(Input.GetAxis("Horizontal")) <= 0.1f)
        {
            P.RunDot.Stop();
        }
    }

    void PlayJumpDot()
    {
        if (P.isInGround && !P.JumpDot.isPlaying)
        {
            P.JumpDot.Play();
        }
    }

    void Jump()
    {
        if (P.jumpTimes <= 0) return;
        P.jumpTimes--;
        if (P.isClibing)
        {
            P.isClibingJump = true;
            int i;
            if (P.isinLeftWall)
            {
                i = 1;
            }
            else
            {
                i = -1;
            }
            StartCoroutine(ClibJump(i));
            return;
        }
        TypeEventSystem.Global.Send<PlayerJumpEvt>();
        PlayJumpDot();
        P.rb.velocity = new Vector2(P.rb.velocity.x, 1 * P.jumpForce);
    }

    IEnumerator ClibJump(int faceIndex)
    {
        TypeEventSystem.Global.Send<PlayerJumpEvt>();
        P.ts.localScale = new Vector3(faceIndex, 1, 1);
        P.rb.velocity = new Vector2(P.jumpForce * faceIndex *2.2f , P.jumpForce * 2.4f);
        DOTween.To((value) => P.rb.drag = value, 0, 8, P.dashduringTime);
        yield return new WaitForSeconds(P.dashduringTime);
        StartCoroutine(ClibJumpOver());
        P.rb.drag = 0;
        P.isClibingJump = false;
    }

    IEnumerator ClibJumpOver()
    {
        P.isClibingJumpOver = true;
        yield return new WaitForSeconds(2f);
        P.isClibingJumpOver = false;
    }

    

    void Dash()
    {
        if ((Mathf.Abs(P.rb.velocity.x) < 0.1f && P.isInGround) || !P.canDash || P.isClibing) return;
        
        P.isDashing = true;
        StartCoroutine(Dashing());
        
        

        // P.rb.velocity = Vector2.zero;
        // Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        // P.rb.velocity = new Vector2(dir.x * P.dashForce, (float)(dir.y * P.dashForce * 0.5));
    }

    IEnumerator Dashing()
    {
        TypeEventSystem.Global.Send<PlayerDashOnceTimeEvt>();
        P.productDashShaow = true;
        P.rb.gravityScale = 0;
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            P.rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * P.dashForce ,Input.GetAxis("Vertical") * P.dashForce * 0.3f);
        }
        else
        {
            P.rb.velocity = new Vector2(P.ts.localScale.x * P.dashForce ,Input.GetAxis("Vertical") * P.dashForce * 0.3f);
        }
        DOTween.To((value) => P.rb.drag = value, 0, 10, 0.2f);
        
        yield return new WaitForSeconds(P.dashduringTime);
        P.productDashShaow = false;
        P.rb.drag = 0;
        P.rb.gravityScale = 1;
        P.isDashing = false;
        P.dashStartTime = Time.time;
        P.canDash = false;
    }

    void DashCoolDown()
    {
        if (Time.time > P.dashCD + P.dashStartTime)
        {
            P.canDash = true;
        }
    }

    void DashShadow()
    {
        if (P.productDashShaow)
        {
            TypeEventSystem.Global.Send<PlayerDashEvt>();
        }
    }

    void Climb(int faceIndex)
    {
        if (P.isClibingJump == false) P.ts.localScale = new Vector3(faceIndex, 1, 1);
        if (!P.isClibing && (((Math.Abs(P.climbSlideSpeed - P.climbSlideMaxSpeed)) < 0.1f) || P.climbSlideSpeed <= 0.1f)){ 
            DOTween.To((value) => P.climbSlideSpeed = value
            , 0, P.climbSlideMaxSpeed, 1.5f);
        }
        P.isClibing = true;
        if (P.isClibingJump == false)
        {
            if (faceIndex == 1 && Input.GetAxis("Horizontal") <0 ) return;
            if (faceIndex == -1 && Input.GetAxis("Horizontal") >0 ) return;
            P.rb.velocity = new Vector2(0, P.climbSlideSpeed);
        }
        
    }


     private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere((Vector2)P.ts.position+P.checkGoundOffset,P.checkGoundRadius);
        Gizmos.DrawWireSphere((Vector2)P.ts.position+P.checkLeftWallOffset,P.checkLeftWallRadius);
        Gizmos.DrawWireSphere((Vector2)P.ts.position+P.checkRightWallOffset,P.checkRightWallRadius);
    }

    void SceneStateManger()
    {
        if (!Physics2D.OverlapCircle((Vector2)P.ts.position + P.checkGoundOffset, P.checkGoundRadius, LayerMask.GetMask("Ground"))
            && !Physics2D.OverlapCircle((Vector2)P.ts.position + P.checkLeftWallOffset, P.checkLeftWallRadius, LayerMask.GetMask("Wall"))
            && !Physics2D.OverlapCircle((Vector2)P.ts.position + P.checkRightWallOffset, P.checkRightWallRadius, LayerMask.GetMask("Wall")))
        {
            P.isInGround = false;
            P.isinSky = true;
            P.isinLeftWall = false;
            P.isinRightWall = false;
        }
        if (P.isClibingJump)
        {
            P.isinLeftWall = false;
            P.isinRightWall = false;
            return;
        }
        if (Physics2D.OverlapCircle((Vector2)P.ts.position + P.checkLeftWallOffset, P.checkLeftWallRadius, LayerMask.GetMask("Wall")))
        {
            P.isInGround = false;
            P.isinSky = false;
            P.isinLeftWall = true;
            P.isinRightWall = false;
            StopCoroutine(ClibJumpOver());
            P.isClibingJumpOver = false;
        }
        if (Physics2D.OverlapCircle((Vector2)P.ts.position + P.checkRightWallOffset, P.checkRightWallRadius, LayerMask.GetMask("Wall")))
        {
            P.isInGround = false;
            P.isinSky = false;
            P.isinLeftWall = false;
            P.isinRightWall = true;
            StopCoroutine(ClibJumpOver());
            P.isClibingJumpOver = false;
        }
        if (Physics2D.OverlapCircle((Vector2)P.ts.position + P.checkGoundOffset, P.checkGoundRadius, LayerMask.GetMask("Ground")))
        {
            P.isInGround = true;
            P.isinSky = false;
            P.isinLeftWall = false;
            P.isinRightWall = false;
            P.isClibingJumpOver = false;
        }
    }

    void PlayerStateManager()
    {
        if (P.isDashing || P.isInGround || P.isJump || P.isFalling || P.isinSky)
        {
            P.isClibing = false;
        }
        if ((P.isinLeftWall || P.isInGround ||P.isinRightWall) && P.isJump)
        {
            P.jumpTimes = 1;
            P.isJump = false;
        }

        if (Input.GetButtonDown("Jump")) P.isJump = true;
    }

    void PlayerAniManager()
    {
        //朝向
        if (Input.GetAxis("Horizontal") > 0 && !P.isClibing && !P.isClibingJump)
        {
            P.ts.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetAxis("Horizontal") < 0 && !P.isClibing && !P.isClibingJump)
        {
            P.ts.localScale = new Vector3(-1, 1, 1);
        }


        //奔跑动画
        if (Mathf.Abs(P.rb.velocity.x) > 0.1f && P.isInGround)
        {
            P.ar.Play("Run");
        }

        //跳跃
        if (P.rb.velocity.y > 0.1f && !P.isClibing)
        {
            P.ar.Play("Jump");
        }

        //掉落
        if (P.rb.velocity.y < -0.1f && !P.isClibing)
        {
            P.ar.Play("Fall");
        }

        //攀爬
        if (P.isClibing)
        {
            P.ar.Play("Climb");
        }

        //待机
        if (Mathf.Abs(P.rb.velocity.x) < 0.1f && P.isInGround)
        {
            P.ar.Play("Idle");
        }

        //冲刺
        if (P.isDashing)
        {
            P.ar.Play("Jump");
        }
    }

    public IArchitecture GetArchitecture()
    {
        return MyGame.Interface;
    }
}