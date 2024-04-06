using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private Animator _animator;

    private float _currentHealth;
    private bool _isDead;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (_isDead) return;

        _currentHealth -= damage;

        if (_currentHealth < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;
        _animator.SetTrigger("Dead");
    }
}
