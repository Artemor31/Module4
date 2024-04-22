using System.Collections;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private MeleeNavMeshMover _mover;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private Role _role;
    [SerializeField] private Loot _loot;
    [SerializeField] private float _desappearTimer;

    private Health _player;

    private void Start()
    {
        _player = FindObjectOfType<Motion>().GetComponent<Health>();

        _health.Init(_role.Health);
        _attacker.Init(_role.Weapon);
        _health.OnHealthChange += OnHealthChange;
    }

    private void OnHealthChange(Health health)
    {
        if (health.IsDead)
        {
            _health.OnHealthChange -= OnHealthChange;
            StartCoroutine(DestroyBody());
        }
    }

    private IEnumerator DestroyBody()
    {
        yield return new WaitForSeconds(_desappearTimer);
        SpawnLoot();
        Destroy(gameObject);
    }

    private void SpawnLoot()
    {
        Loot loot = Instantiate(_loot, transform.position, Quaternion.identity);
        loot.Init(_attacker.Weapon);
    }

    private void Update()
    {
        if (_player.IsDead) return;
        if (_health.IsDead) return;

        _attacker.SetTarget(_player);

        if (_attacker.TargetInRange())
        {
            _mover.Stop();
            _mover.LookAt();
            _attacker.Attack();
        }
        else
        {
            _mover.MoveTo(_player);
        }
    }
}
