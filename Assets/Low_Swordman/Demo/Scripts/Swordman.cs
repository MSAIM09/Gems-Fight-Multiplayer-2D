using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Swordman : PlayerController
{
    private float m_MoveY;
    PhotonView view;
    /*public Text timerText;
    private float timeRemaining = 60f;
    private bool timerRunning = true;
    public CoinsCount CoinsCountScript;
    public GameObject gameOverPanel;*/
    private void Start()
    {
        m_CapsulleCollider = this.transform.GetComponent<CapsuleCollider2D>();
        m_Anim = this.transform.Find("model").GetComponent<Animator>();
        m_rigidbody = this.transform.GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (view.IsMine)
        {
            checkInput();

            if (m_rigidbody.velocity.magnitude > 30)
            {
                m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x - 0.1f, m_rigidbody.velocity.y - 0.1f);
            }
        }

        /*if (timerRunning)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                timerRunning = false;
                Destroy(gameObject);
                Debug.Log("Player destroy");
                gameOverPanel.SetActive(true);
                Debug.Log("Time, Gameover");
            }
        }*/

    }
    /*void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{0:60}", minutes, seconds);
    }*/

    public void checkInput()
    {
        m_MoveX = Input.GetAxis("Horizontal");
        m_MoveY = Input.GetAxis("Vertical");

        // Handle sitting and standing
        if (Input.GetKeyDown(KeyCode.S)) // Sit action
        {
            IsSit = true;
            m_Anim.Play("Sit");
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            m_Anim.Play("Idle");
            IsSit = false;
        }

        // Prevent other animations during sit or die states
        if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Sit") || m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentJumpCount < JumpCount) // Jump action
                {
                    DownJump();
                }
            }
            return;
        }

        GroundCheckUpdate();

        if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (Input.GetKey(KeyCode.Mouse0)) // Attack action
            {
                m_Anim.Play("Attack");
            }
            else
            {
                if (m_MoveX == 0 && m_MoveY == 0)
                {
                    if (!OnceJumpRayCheck)
                        m_Anim.Play("Idle");
                }
                else
                {
                    m_Anim.Play("Run");
                }
            }
        }

        if (Input.GetKey(KeyCode.Alpha1)) // Die action
        {
            m_Anim.Play("Die");
        }

        // Move the player based on input
        Vector2 moveDirection = new Vector2(m_MoveX, m_MoveY).normalized;
        transform.Translate(moveDirection * MoveSpeed * Time.deltaTime);

        // Flip the character sprite based on the horizontal movement direction
        if (m_MoveX != 0)
        {
            Filp(m_MoveX < 0);
        }

        // Jump action
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                if (currentJumpCount < JumpCount) // 0, 1
                {
                    if (!IsSit)
                    {
                        prefromJump();
                    }
                    else
                    {
                        DownJump();
                    }
                }
            }
        }
    }

    protected override void LandingEvent()
    {
        if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Run") && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            m_Anim.Play("Idle");
    }

    private void Filp(bool facingLeft)
    {
        Vector3 scale = transform.localScale;
        scale.x = facingLeft ? -1 : 1;
        transform.localScale = scale;
    }
}
