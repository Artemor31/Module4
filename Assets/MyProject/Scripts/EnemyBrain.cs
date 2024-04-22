using System;
using System.Collections;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public event Action<EnemyBrain> OnDied;
    public int ExpReward { get; private set; }
    public int GoldReward { get; private set; }

    [SerializeField] private Health _health;
    [SerializeField] private MeleeNavMeshMover _mover;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private Loot _loot;
    [SerializeField] private float _desappearTimer;

    private Health _player;
    private Item _lootItem;

    public void Init(Player player, EnemyData data)
    {
        _player = player.GetComponent<Health>(); 
        _health.Init(data.Health);
        _attacker.Init(data.Weapon);
        ExpReward = data.Exp;
        GoldReward = data.Gold;
        _lootItem = data.LootItem;
        _health.OnHealthChange += OnHealthChange;
    }

    private void OnHealthChange(Health health)
    {
        if (health.IsDead)
        {
            _health.OnHealthChange -= OnHealthChange;
            StartCoroutine(DestroyBody());
            OnDied?.Invoke(this);
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
        if (_lootItem == null) return;

        Loot loot = Instantiate(_loot, transform.position, Quaternion.identity);
        loot.Init(_lootItem);
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
