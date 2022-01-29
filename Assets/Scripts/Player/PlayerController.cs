using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO.MemoryMappedFiles;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Color = UnityEngine.Color;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    public float speed = 6.8f;
    public float jumpSpeed = 20f;
    public float movement = 0f;
    [Range(-2.0f, 2.0f)] 
    public float _movement;
    private bool doubleJump;
    public Rigidbody2D rigidBody;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public bool finishLevel = false;
    public bool isTouchingGround;
    public Animator playerAnimation;
    public bool HurtBlink;
    public GameObject EnemyTrigger;
    public Collider2D collider2d;
    public bool Keyboard = true;
    public bool isFalling;
    public GameObject failedMenu;
    public Vector3 cpPosition;
    private IEnumerator dyingCouroutine;
    public bool dead = false;
    public GameObject stompBox;
    private IEnumerator blinkRoutine;
    private SpriteRenderer _renderer;
    private GameObject slime;
    public float Slimespeed = 5f;
    public GameObject slimePrefab;
    private bool canAttact = true;
    public float timeBetweenSlimes = 6;
    bool rightposition = true;
    private float fJumpPressedRemember = 0;
    float fJumpPressedRememberTime = 0.2f;
    private float fGroundedRemember = 0;
    float fGroundedRememberTime = 0.13f;
    [Range(0.0f, 1.0f)] 
    public float fHorizontalDampingWhenStopping = 0.8f;
    [Range(0.0f, 1.0f)] 
    public float fHorizontalDampingWhenTurning = 0.8f;
    [Range(0.0f, 1.0f)] 
    public float fHorizontalDampingBasic=0.8f;

    public AudioClip[] audio;
    private AudioSource audiosource;
    private bool tripleJump = false;
    private bool allowTripleJump = false;
    public GameObject particlePrefab;
    public GameObject particleParent;
    private GameObject particleObject;
    void Start()
    {
        gameObject.GetComponent<SpriteResolver>().SetCategoryAndLabel(LevelManager.characterName[LevelManager.choosedCharacter],"idle");
        dead = false;
        tripleJump = _Level.tripleJump;
        switch (_Level.fastershooting)
        {
            case 0:
                Slimespeed = 5;
                timeBetweenSlimes = 20;
                break;
            case 1:
                Slimespeed = 6.8f;
                timeBetweenSlimes = 10f;
                break; 
            case 2:
                Slimespeed = 9;
                timeBetweenSlimes = 5;
                break;
            case 3:
                Slimespeed = 12;
                timeBetweenSlimes = 0.5f;
                break;
        }
        dyingCouroutine = IsDying();
        if (instance == null)
            instance = this;
       rigidBody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        HurtBlink = false;
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        audiosource = GetComponent<AudioSource>();
    }
    

    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);

        if (Keyboard)
        {
            movement = Input.GetAxis("Horizontal");
            if (Input.GetButtonDown("Jump"))
            {
                JumpButton();
            }

            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
            }
        }
        else
        {
            movement = CrossPlatformInputManager.GetAxis("Horizontal");
        }
      
        
       _movement += movement;

       if((Mathf.Abs(movement)) < 0.01f)
           _movement *= Mathf.Pow(1f - fHorizontalDampingWhenStopping, Time.deltaTime * 10f);
       else if((Mathf.Sign(movement)) != Mathf.Sign(_movement))
       {
           _movement*=Mathf.Pow(1f - fHorizontalDampingWhenTurning, Time.deltaTime * 10f);
       }
       else
       {
           _movement*=Mathf.Pow(1f - fHorizontalDampingBasic, Time.deltaTime * 10f);
       }

       if (_movement > 1)
           _movement = 1;
       else if (_movement < -1)
           _movement = -1;
       rigidBody.velocity = new Vector2(_movement * speed, rigidBody.velocity.y);
       

       if (movement > 0f)
        {
            rightposition = true;
            transform.localScale = new Vector2(1.1f, 1.1f);
        }
        else if (movement < 0f)
        {
            rightposition = false;
            transform.localScale = new Vector2(-1.1f, 1.1f);
        }

        if (isTouchingGround)
        {
            doubleJump = true;
            fGroundedRemember=fGroundedRememberTime;
        }
       
        else if (doubleJump && Input.GetButtonDown("Jump"))
        {
            jumpSpeed = 16f;
            Jump();
            allowTripleJump = true;
            doubleJump = false;
        }
        else if (tripleJump && allowTripleJump && Input.GetButtonDown("Jump"))
        {
            jumpSpeed = 16f;
            Jump();
            allowTripleJump = false;
        }
        if (rigidBody.velocity.y < -1)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
        
        playerAnimation.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));
        playerAnimation.SetBool("OnGround", isTouchingGround);
        playerAnimation.SetBool("isFalling", isFalling);

        fJumpPressedRemember -=Time.deltaTime;
        fGroundedRemember -= Time.deltaTime;
        if (fJumpPressedRemember > 0 && fGroundedRemember>0)
        {
            doubleJump = true;
            fJumpPressedRemember = 0;
            fGroundedRemember = 0;
            Jump();
        }
        else if (fJumpPressedRemember > 0 && doubleJump)
        {
            doubleJump = false;
            allowTripleJump = true;
            fJumpPressedRemember = 0;
            Jump();
        }
        else if(fJumpPressedRemember>0 && allowTripleJump && tripleJump)
        {
            allowTripleJump = false;
            fJumpPressedRemember = 0;
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (!isFalling) {
            stompBox.SetActive (false);
        } else {
            stompBox.SetActive (true);
        }

        if (speed > 10 && _movement >= 0.5f)
        {
            MakeSmoke();
        }
    }

    public void JumpButton()
    {
        jumpSpeed = 16f;
        fJumpPressedRemember = fJumpPressedRememberTime;
       
    }


    void Jump()
    {
        audiosource.PlayOneShot(audio[0],1);
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        playerAnimation.Play("jump", -1,0f);
        
     MakeSmoke();
    }

    public void MakeSmoke()
    {
        particleObject = Instantiate(particlePrefab, new Vector3(particleParent.transform.position.x, particleParent.transform.position.y),
            Quaternion.identity) as GameObject;
    }

    public void Attack()
    {
        if (canAttact && ScoreManager.instance.Slime>0)
        {
            canAttact = false;
            ScoreManager.instance.changeSlime(-1);
            slime = Instantiate(slimePrefab, new Vector3(transform.position.x, transform.position.y -0.1f),
                Quaternion.identity) as GameObject;
            if (rightposition)
            {
                slime.GetComponent<slimeScript>()._speed = Slimespeed;
                slime.transform.localScale = new Vector2(1, 1);

            }
            else
            {
                slime.GetComponent<slimeScript>()._speed = -Slimespeed;
                slime.transform.localScale = new Vector2(-1, 1);
            }

            StartCoroutine(WaitforSlime());
        }
    }
    
    



    IEnumerator WaitforSlime()
    {
        yield return new WaitForSeconds(timeBetweenSlimes*Time.deltaTime);
        canAttact = true;
    }

   private void OnTriggerEnter2D(Collider2D other)
   {
       if (other.gameObject.CompareTag("FallDetector"))
        {
           Death();
        }

       if (other.CompareTag("Checkpoint"))
       {
           cpPosition = transform.localPosition;

       }
    }

   public void Hurt()
   {
       if (!HurtBlink )
       {
           HurtBlink = true;
           blinkRoutine = Wait();
           StartCoroutine(blinkRoutine);
       }
   }

        private IEnumerator Wait()
        {
            HurtBlink = true;
            EnemyTrigger.SetActive(false);
            HeartScript.instance.SubLive();
            for(int i=0; i<10; i++)
            {
            _renderer.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.1f);
            _renderer.color = new Color(0.8f, 0.8f, 0.8f, 0.7f);
            yield return new WaitForSeconds(0.1f);
            }
            _renderer.color = new Color(1, 1, 1, 1);
          
            HurtBlink = false;
   
         EnemyTrigger.SetActive(true);
        }




        public void Death()
        {
            if (!dead)
            {
                if(blinkRoutine!=null)
                StopCoroutine(blinkRoutine);
                _renderer.color = new Color(1, 1, 1, 1);
                dead = true;
                CameraController.instance.PlayerDying();
                HeartScript.instance.SubLive();
                HeartScript.instance.SubLive();
                HeartScript.instance.SubLive();
                playerAnimation.SetBool("isDying", true);
                rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                rigidBody.gravityScale = 0;
                collider2d.enabled = false;
                dyingCouroutine = IsDying();
                StartCoroutine(dyingCouroutine);
            }
        }

        private IEnumerator IsDying()
        {
            yield return new WaitForSeconds(2);
                Vector3 FirstPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                while (transform.localPosition.y >= FirstPosition.y - 9f)
                {
                    transform.localPosition = new Vector2(transform.localPosition.x,
                        transform.localPosition.y - 15 * Time.deltaTime);
                    yield return new WaitForSeconds(0.5f * Time.deltaTime);
                }

                failedMenu.SetActive(true);
                failedMenu.GetComponent<FailedMenu>().Awake();
                dead = false;
                StopCoroutine(dyingCouroutine);
        }


        public void Checkpoint()
        {
            transform.localPosition = new Vector3(cpPosition.x, cpPosition.y+0.3f, cpPosition.z);
            dead = false;
            HurtBlink = false;
            if(blinkRoutine!=null)
                StopCoroutine(blinkRoutine);
            _renderer.color = new Color(1, 1, 1, 1);
            EnemyTrigger.SetActive(true);
        }
}

