using System.Collections;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private MeleeNavMeshMover _mover;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private Role _role;
    [SerializeField] private Loot _loot;
    [SerializeField] private float _destroyDelay;

    private Health _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>().GetComponent<Health>();

        _health.Init(_role.Health);
        _attacker.SetWeapon(_role.Weapon);
        _attacker.SetTarget(_player);

        _health.OnHealthChange += OnHealthChange;
    }

    private void OnHealthChange(Health health)
    {
        if (health.IsDead)
        {
            _health.OnHealthChange -= OnHealthChange;
            StartCoroutine(DestroyAfterDelay());
        }
    }

    public IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(_destroyDelay);
        Loot loot = Instantiate(_loot, transform.position, Quaternion.identity);
        loot.Init(_role.Weapon);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (_player.IsDead) return;
        if (_health.IsDead ) return;

        if (_attacker.TargetInRange())
        {
            _mover.Stop();
            _mover.LookAt(_player);
            _attacker.Attack();
        }
        else 
        {
            _mover.MoveTo(_player);
        }
    }
}
