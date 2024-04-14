using UnityEngine;

public class Attacker : MonoBehaviour
{    
    public bool CooldownUp => _attackTime <= 0;
    public bool IsAttacking {  get; private set; }

    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _damageMask;
    [SerializeField] private Transform _hand;

    private Collider[] _hits = new Collider[3];
    private Weapon _weapon;
    private float _attackTime;

    public void Init(Weapon weapon)
    {
        _weapon = weapon;
        ResetAttackTimer();
        Instantiate(_weapon.Prefab, _hand);

        if (_weapon.OverrideController != null)
            _animator.runtimeAnimatorController = _weapon.OverrideController;
    }

    public bool TargetInRange(Health target) =>
        Vector3.Distance(transform.position, target.transform.position) < _weapon.Range;

    public void Attack()
    {
        if (CooldownUp)
        {
            AnimateAttack();
            ResetAttackTimer();
            AttackNearEnemies();
        }
    }

    void Update()
    {
        _attackTime -= Time.deltaTime;
        IsAttacking = _weapon.AttackCooldown - _attackTime < 1;
    }

    private void ResetAttackTimer() => _attackTime = _weapon.AttackCooldown;

    private void AnimateAttack()
    {
        var index = Random.Range(0, 2);
        _animator.SetInteger("AttackIndex", index);
        _animator.SetTrigger("Attacking");
    }

    private void AttackNearEnemies()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, _weapon.Range, _hits, _damageMask);

        for (int i = 0; i < count; i++)
        {
            if (_hits[i].TryGetComponent<Health>(out var health))
            {
                health.TakeDamage(_weapon.Damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _weapon.Range);
    }
}
