using System.Timers;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    private bool CanAttack => _attackTime <= 0;

    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _damageMask;

    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _damage;
    [SerializeField] private float _radius;

    Collider[] _hits = new Collider[1];
    private float _attackTime;

    private void Start() => ResetAttackTimer();

    public bool TargetInRange(Health target) =>
        Vector3.Distance(transform.position, target.transform.position) < _radius;

    public void Attack()
    {
        if (CanAttack)
        {
            _animator.SetTrigger("Attacking");
            ResetAttackTimer();
            AttackNearEnemies();
        }
    }

    private void AttackNearEnemies()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, _hits, _damageMask);

        for (int i = 0; i < count; i++)
        {
            if (_hits[i].TryGetComponent<Health>(out var health))
            {
                health.TakeDamage(_damage);
            }
        }
    }
    void Update() => _attackTime -= Time.deltaTime;

    private void ResetAttackTimer() => _attackTime = _attackCooldown;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}