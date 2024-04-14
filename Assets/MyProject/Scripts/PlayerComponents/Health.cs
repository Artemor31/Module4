using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public bool IsDead => _currentHealth <= 0;

    private float _currentHealth;

    public void Init(float _maxHealth) => _currentHealth = _maxHealth;

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die() => _animator.SetTrigger("Dead");
}
