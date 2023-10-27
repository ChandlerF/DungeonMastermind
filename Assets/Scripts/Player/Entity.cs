using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    public float MaxHealth = 20f, CurrentHealth, MoveSpeed = 5f, MoveSpeedMultiplier = 1f;

    public LayerMask FriendlyLayers, HostileLayers;

    private float _invincibleTimerStart = 0.2f, _invincibleTimer;
    private bool _canTakeDamage = true;

    private Color _flashColor = Color.white;
    private Material _startMaterial, _whiteMaterial;
    private SpriteRenderer _sr;
    public Rigidbody2D _rb;

    private List<Color> _spriteColors = new List<Color>();



    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();


        _whiteMaterial = Resources.Load<Material>("FlashWhite");



        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
            _spriteColors.Add(sr.color);
        }


        _startMaterial = _sr.material;
        CurrentHealth = MaxHealth;
        _invincibleTimer = _invincibleTimerStart;
    }



    protected virtual void Update()
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
        

        _rb.AddForce((_rb.drag * (amount / 8)) * (transform.position - location).normalized, ForceMode2D.Impulse);
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
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
            sr.color = _flashColor;
            sr.material = _whiteMaterial;
        }



        _sr.color = _flashColor;
        _sr.material = _whiteMaterial;


        yield return new WaitForSeconds(duration);


        int i = 0;
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
            sr.color = _spriteColors[i];
            sr.material = _startMaterial;

            i++;
        }


        _sr.material = _startMaterial;
    }
}
