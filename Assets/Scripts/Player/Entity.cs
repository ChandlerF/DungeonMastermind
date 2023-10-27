using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    public float MaxHealth = 20f, CurrentHealth, MoveSpeed = 5f, MoveSpeedMultiplier = 1f;

    public LayerMask FriendlyLayers, HostileLayers;

    private float _invincibleTimerStart = 0.2f, _invincibleTimer;
    private bool _canTakeDamage = true;

    private void Start()
    {
        CurrentHealth = MaxHealth;
        _invincibleTimer = _invincibleTimerStart;
    }



    private void Update()
    {
        InvincibleTimer();
    }



    public virtual void Damage(float amount, Vector3 location) 
    {
        if (!_canTakeDamage) return;

        CurrentHealth -= amount;

        if (CurrentHealth <= 0)
        {
            //Death
            Destroy(gameObject);
        }
        

        GetComponent<Rigidbody2D>().AddForce((MoveSpeed * amount) * (transform.position - location).normalized, ForceMode2D.Impulse);
        _canTakeDamage = false;
        _invincibleTimer = _invincibleTimerStart;
    }


    private void InvincibleTimer()
    {
        if (_invincibleTimer < 0)
        {
            _canTakeDamage = true;
        }
        else
        {
            _invincibleTimer -= Time.deltaTime;
        }
    }
}
