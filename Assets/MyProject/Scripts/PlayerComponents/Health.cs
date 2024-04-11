using UnityEngine;

public class Health : MonoBehaviour
{
    public bool IsDead => _currentHealth <= 0;

    [SerializeField] private Animator _animator;
    [SerializeField] private float _maxHealth;
    private float _currentHealth;

    private void Start() => _currentHealth = _maxHealth;

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
