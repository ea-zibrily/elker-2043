using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region RequireComponent

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]

#endregion
public class EleController : ObserverSubjects
{
    [Header("Controller Component")]
    public PlayerData playerDataSO;
    [SerializeField] private Vector2 playerMoveInput;
    [SerializeField] private bool isRight;

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
        myAnim = GetComponent<Animator>();
    }
    
    void Start()
    {
        isRight = true;
    }
    
    private void FixedUpdate()
    {
        EleMove();
        EleAnimation();
        EleDirection();
    }

    private void Update()
    {
        EleJump();
    }

    #endregion

    #region Tsukuyomi Methods

    private void EleMove()
    {
        float moveX;
        moveX = Input.GetAxisRaw("Horizontal");

        playerMoveInput = new Vector2(moveX, playerMoveInput.y);
        playerMoveInput.Normalize();
        
        myRb.velocity = playerMoveInput * playerDataSO.PlayerSpeed;
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
        if (Input.GetKeyDown(KeyCode.W) && !IsGround())
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
