using System;
using UnityEngine;

[Serializable]
public struct AttackInfo
{
    // Physics / Logic data
    public int Strength;
    public Vector2 ImpactDirection;
    
    // Frame data
    public int HitStun;
    public int BlockStun;
    
}

[RequireComponent(typeof(Collider2D))]
public class AttackScript : MonoBehaviour
{
    [SerializeField] private AttackInfo attackInfo;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IDamageable damageable))
            return;
        
        damageable.Damage(attackInfo);
    }
}
