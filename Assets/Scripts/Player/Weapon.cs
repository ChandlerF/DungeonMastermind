using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public bool CanAttack = true;
    [SerializeField] private string _fxName;
    private int _fxState;
    [SerializeField] private AudioSource _sound;
    [SerializeField] private float _damageAmount, _attackDelay;
    private float _currentAttackDelay;
    private Animation _animation;
    public LayerMask HostileLayers;
    [SerializeField] private Animator _fxAnim;


    private void Start()
    {
        _currentAttackDelay = _attackDelay;
        _fxState = Animator.StringToHash(_fxName);
        _animation = GetComponent<Animation>();
    }


    private void Update()
    {
        if(_currentAttackDelay < 0)
        {
            CanAttack = true;
            _currentAttackDelay = _attackDelay;
        }
        else
        {
            _currentAttackDelay -= Time.deltaTime;
        }
    }






    public void WeaponAttack()
    {
        if (!CanAttack) return;


        _animation.Play("SwordSwing1");
        _fxAnim.Play(_fxState);

        //Play Sound
        // Enable hitbox

        CanAttack = false;
    }



    /*
    private void AnimCheck()
    {
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



        int LockState(int state, float time)
        {
            _lockedTime = Time.time + time;
            return state;
        }
    }
    */




    private void OnTriggerEnter2D(Collider2D col)
    {
        //If Entity and on Layermask
        if (col.gameObject.TryGetComponent<Entity>(out Entity entity) &&
           (HostileLayers.value & 1 << col.gameObject.layer) == 1 << col.gameObject.layer)
        {
            entity.Damage(_damageAmount, transform.position);
        }
    }
}
