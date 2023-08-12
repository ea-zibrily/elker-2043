using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

#region RequireComponent

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

#endregion
public class EleController : MonoBehaviour
{
    [Header("Controller Component")]
    public PlayerData playerDataSO;
    [SerializeField] private Vector2 playerMoveInput;
    [SerializeField] private bool isRight;
    public bool IsCaught {get; set;}

    [Header("Ground Checker Component")]
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float groundCheckerRadius;
    public LayerMask groundLayer;

    [Header("Reference")] 
    private Rigidbody2D myRb;
    private Animator myAnim;
    private EleDetector eleDetector;
    private EleEventHandler eleEventHandler;

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();
        myAnim = GetComponentInChildren<Animator>();
        eleDetector = GetComponentInChildren<EleDetector>();
        eleEventHandler = GetComponentInChildren<EleEventHandler>();
    }

    private void OnEnable()
    {
        eleEventHandler.OnFootStepSound += PlayFootstepSound;
    }
    
    private void OnDisable()
    {
        eleEventHandler.OnFootStepSound -= PlayFootstepSound;
    }

    private void Start()
    {
        isRight = true;
    }
    
    private void FixedUpdate()
    {
        EleMove();
    }

    private void Update()
    {
        EleJump();
        EleAnimation();
        EleDirection();
    }

    #endregion

    #region Tsukuyomi Methods

    private void EleMove()
    {
        if (IsCaught)
        {
            return;
        }
        
        float moveX;
        moveX = Input.GetAxisRaw("Horizontal");
        playerMoveInput = new Vector2(moveX, 0);
        playerMoveInput.Normalize();
        
        myRb.velocity = new Vector2(playerMoveInput.x * playerDataSO.PlayerSpeed, myRb.velocity.y);
    }

    private void EleDirection()
    {
        if (playerMoveInput.x < 0 && isRight)
        {
            EleFlip();
        }
        if (playerMoveInput.x > 0 && !isRight)
        {
            EleFlip();
        }
    }
    
    private void EleAnimation()
    {
        if (playerMoveInput.x != 0)
        {
            myAnim.SetBool("isMove", true);
        }
        else
        {
            myAnim.SetBool("isMove", false);
        }
    }
    
    private void EleFlip()
    {
        isRight = !isRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void EleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGround())
        {
            myRb.AddForce(Vector2.up * playerDataSO.PlayerJumpForce, ForceMode2D.Impulse);
        }
    }

    public void ResumeEleMovement()
    {
        IsCaught = false;
        eleDetector.gameObject.SetActive(true);
    }

    public void StopEleMovement()
    {
        IsCaught = true;
        eleDetector.gameObject.SetActive(false);
        myRb.velocity = Vector2.zero;
        playerMoveInput.x = 0;
    }
    
    private bool IsGround()
    {
        return Physics2D.OverlapCircle(groundChecker.position, groundCheckerRadius, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundChecker.position, groundCheckerRadius);
    }

    private void PlayFootstepSound()
    {
        if (IsGround())
        {
            FindObjectOfType<AudioManager>().PlayAudio(SoundEnum.SFX_Ele);
        }
    }
   

    #endregion
}
