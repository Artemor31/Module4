using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _bloodVfx;
    public bool IsDead => CurrentHealth <= 0;
    public event Action<Health> OnHealthChange;

    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }

    public void Init(float _maxHealth)
    {
        MaxHealth = _maxHealth;
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        OnHealthChange?.Invoke(this);

        Instantiate(_bloodVfx, transform.position + Vector3.up, Quaternion.identity);
        _animator.SetTrigger("Hited");

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die() => _animator.SetTrigger("Dead");
}
