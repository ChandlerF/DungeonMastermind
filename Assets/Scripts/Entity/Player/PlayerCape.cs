using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCape : MonoBehaviour
{
    private static readonly int _idle = Animator.StringToHash("CapeIdle");
    private static readonly int _run = Animator.StringToHash("CapeRun");

    private Animator _animator;
    private PlayerMovement _playerScript;

    void Start()
    {
        _playerScript = transform.parent.GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
    }


    void Update()
    {
        AnimCheck();
    }


    private void AnimCheck()
    {
        var state = _playerScript.MovementInput == Vector2.zero ? _idle : _run;

        _animator.CrossFade(state, 0, 0);
    }
}
