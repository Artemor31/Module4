using UnityEngine;

public class Attacker : MonoBehaviour
{    
    public bool CooldownUp => _attackTime <= 0;
    public bool IsAttacking {  get; private set; }
    public Weapon Weapon => _weapon;

    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _damageMask;
    [SerializeField] private Transform _handLeft;
    [SerializeField] private Transform _handRight;

    private Collider[] _hits = new Collider[3];
    private Weapon _weapon;
    private GameObject _weaponObject;
    private float _attackTime;
    private Health _target;

    public void Init(Weapon weapon)
    {
        if (_weaponObject != null)        
            Destroy(_weaponObject);        

        _weapon = weapon;
        _weaponObject = Instantiate(_weapon.Prefab, _weapon.Grip == GripType.Left ? _handLeft : _handRight);
        ResetAttackTimer();

        if (_weapon.OverrideController != null)
            _animator.runtimeAnimatorController = _weapon.OverrideController;
    }

    public void SetTarget(Health target)
    {
        _target = target;
    }

    public bool TargetInRange()
    {
        if (_target == null) return false;
        return Vector3.Distance(transform.position, _target.transform.position) < _weapon.Range;
    }

    public void Attack()
    {
        if (CooldownUp)
        {
            ResetAttackTimer();
            AnimateAttack();
        }
    }

    // animation event
    public void AttackEvent()
    {
        if (_weapon.Range > 3)
        {
            SpawnProjectile();
        }
        else
        {
            AttackNearEnemies();
        }
    }

    private void SpawnProjectile()
    {
        var projectile = Instantiate(_weapon.ProjectilePrefab, _weaponObject.transform.position, Quaternion.identity);
        projectile.transform.LookAt(_target.transform.position + Vector3.up);
        projectile.Init(_weapon.Damage);
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
        if (_weapon == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _weapon.Range);
    }
}
