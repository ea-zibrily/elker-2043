using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region RequireComponent

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

#endregion
public class EleController : ObserverSubjects
{
    [Header("Controller Component")]
    public PlayerData playerDataSO;
    [SerializeField] private Vector2 playerMoveInput;
    [SerializeField] private bool isRight;
    [HideInInspector] public bool isCaught;

    [Header("Ground Checker Component")]
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float groundCheckerRadius;
    public LayerMask groundLayer;

    [Header("Reference")] 
    private Rigidbody2D myRb;
    private Animator myAnim;

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();
        myAnim = GetComponentInChildren<Animator>();
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
        if (isCaught)
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
            NotifyObservers(ActionEnum.Walk);
        }
        else
        {
            myAnim.SetBool("isMove", false);
            NotifyObservers(ActionEnum.Idle);
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
            NotifyObservers(ActionEnum.Jump);
        }
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

    #endregion
}
