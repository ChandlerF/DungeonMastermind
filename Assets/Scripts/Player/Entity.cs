using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    public float MaxHealth = 20f, CurrentHealth, MoveSpeed = 5f, MoveSpeedMultiplier = 1f;

    public LayerMask FriendlyLayers, HostileLayers;

    private float _invincibleTimerStart = 0.2f, _invincibleTimer;
    private bool _canTakeDamage = true;

    private Color _startColor, _flashColor = Color.white;
    private Material _startMaterial, _whiteMaterial;
    private SpriteRenderer _sr;
    public Rigidbody2D _rb;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }


    private void Start()
    {
        _whiteMaterial = Resources.Load<Material>("FlashWhite");

        _startColor = _sr.color;
        _startMaterial = _sr.material;
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
        

        _rb.AddForce((MoveSpeed * amount) * (transform.position - location).normalized, ForceMode2D.Impulse);
        _canTakeDamage = false;
        _invincibleTimer = _invincibleTimerStart;
        StartCoroutine(FlashWhite(_invincibleTimerStart));
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


    IEnumerator FlashWhite(float duration)
    {
        _sr.color = _flashColor;
        _sr.material = _whiteMaterial;


        yield return new WaitForSeconds(duration);

        _sr.color = _startColor;
        _sr.material = _startMaterial;
    }
}
