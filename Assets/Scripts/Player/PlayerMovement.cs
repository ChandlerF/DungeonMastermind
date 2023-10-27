using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking.Types;
using UnityEngine.SceneManagement;

public class PlayerMovement : Entity
{
    public Vector2 MovementInput = Vector2.zero;
    private Animator _animator;
    private float _lockedTime;
    private int _currentAnimState;
    private Vector3 _startScale;

    #region Animation Strings
    private static readonly int _idle = Animator.StringToHash("PlayerIdle");
    private static readonly int _run = Animator.StringToHash("PlayerRun");

    #endregion


    private void Start()
    {
        _startScale = transform.localScale;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        AnimCheck();
    }

    private void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        _rb.AddForce(MovementInput * (base.MoveSpeed * base.MoveSpeedMultiplier));
    }




    public void MoveInput(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }




    public void Pause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(Time.timeScale == 0) 
                Time.timeScale = 1;
            else
                Time.timeScale = 0f;
        }
    }



    public void RestartScene(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }



    public override void Damage(float amount, Vector3 location)
    {
        base.Damage(amount, location);
    }



    private void AnimCheck()
    {
        FlipSprite();

        int state = AnimState();

        if (_currentAnimState == state) return;

        _animator.CrossFade(state, 0, 0);
        _currentAnimState = state;

    }

    private int AnimState()
    {
        if (Time.time < _lockedTime) return _currentAnimState;

        //Add more important animations at the top of this function

        //if(WannaAttack)  LockState(AttackAnimation, AnimationDuration)


        if (MovementInput == Vector2.zero)
        {
            return _idle;
        }
        else
        {
            return _run;
        }

        int LockState(int state, float time)
        {
            _lockedTime = Time.time + time;
            return state;
        }
    }

    private void FlipSprite()
    {
        if (MovementInput.x < 0)
        {
            transform.localScale = new Vector3(_startScale.x * -1, _startScale.y);
        }
        else if (MovementInput.x > 0)
        {
            transform.localScale = _startScale;
        }
    }

    public void OnPlayerJoined()
    {
        //Add to list on Game Manager
        Debug.Log("ssss");

        
        //SendMessage() is Like Invoke() but for other scripts but on GameObjects instead (ish)
        //Learn: Unity and C# Events, Delegates, Classes, Structs
    }

}
