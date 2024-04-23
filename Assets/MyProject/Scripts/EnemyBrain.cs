using System;
using System.Collections;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public event Action<int, int, EnemyBrain> OnDied;
    [SerializeField] private Health _health;
    [SerializeField] private MeleeNavMeshMover _mover;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private Loot _loot;
    [SerializeField] private float _destroyDelay;

    private Health _player;
    private Enemy _data;

    public void Init(Player player, Enemy enemyData)
    {
        _player = player.GetComponent<Health>();
        _health.Init(enemyData.StartHealth);
        _attacker.SetWeapon(enemyData.Weapon);
        _attacker.SetTarget(_player);
        _data = enemyData;

        _health.OnHealthChange += OnHealthChange;
    }
    private void Update()
    {
        if (_player.IsDead) return;
        if (_health.IsDead) return;

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

    private void OnHealthChange(Health health)
    {
        if (health.IsDead)
        {
            OnDied?.Invoke(_data.Gold, _data.Exp, this);
            _health.OnHealthChange -= OnHealthChange;
            StartCoroutine(DestroyAfterDelay());
        }
    }

    public IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(_destroyDelay);
        Destroy(gameObject);

        if (_data.Loot != null)
        {
            Loot loot = Instantiate(_loot, transform.position, Quaternion.identity);
            loot.Init(_data.Loot);
        }      
    }
}
