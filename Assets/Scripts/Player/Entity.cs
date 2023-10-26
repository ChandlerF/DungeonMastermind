using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    public float MaxHealth = 20f, CurrentHealth, MoveSpeed = 5f, MoveSpeedMultiplier = 1f;

    public LayerMask FriendlyLayers, HostileLayers;

    public virtual void Damage(float amount, Vector2 location) { }

}
