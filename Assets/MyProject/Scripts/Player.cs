using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action<int, int> OnExpChange;
    public event Action<int> OnGoldChange;

    private Progression _progression;

    public PlayerInventory playerInventory { get; private set; }
    private int _gold;
    private int _exp;
    private int _level;

    [SerializeField] private Health _health;
    [SerializeField] private Motion _mover;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private InputManager _input;
    [SerializeField] private LootPicker _lootPicker;
    [SerializeField] private Caster _caster;

    public void Init(Role role, Progression progression)
    {
        InitComponents(role);
        _progression = progression;
        playerInventory = new PlayerInventory();
        playerInventory.AddEquip(Slot.Weapon, role.Weapon);
        _lootPicker.OnItemPicked += OnItemPicked;
    }

    private void OnItemPicked(Item item)
    {
        playerInventory.AddInvent(item);
    }

    private void InitComponents(Role role)
    {
        _health.Init(role.Health);
        _attacker.Init(role.Weapon);
        _input.Init(Camera.main);
    }

    void Update()
    {
        if (_health.IsDead) return;

        if (_input.Attacking)
        {
            _attacker.Attack();
        }
        if (_input.Casting)
        {
            _caster.CastSpell(_input.GetPointerPosition());
        }

        if (!_attacker.IsAttacking)
        {
            _mover.Move(_input.Motion);
        }
    }

    internal void BoostExp(int expReward)
    {
        int expLeft = _progression.ExpForLevel(_level + 1, _exp);
        if (expLeft < expReward)
        {
            _level++;
            _health.AddHealth(_progression.HealthFor(_level));
            _exp = expReward - expLeft;
        }
        else
        {
            _exp += expReward;
        }

        OnExpChange?.Invoke(_exp, _progression.NeedExpFor(_level));
    }

    internal void BoostGold(int goldReward)
    {
        _gold += goldReward;
        OnGoldChange?.Invoke(_gold);
    }

    public PlayerData Save()
    {
        var data = new PlayerData
        {
            Level = _level,
            Exp = _exp,
            Gold = _gold,
            Health = _health.Save()
        };

        return data;
    }

    public void Restore(PlayerData data)
    {
        _level = data.Level;
        _exp = data.Exp;
        _gold = data.Gold;
        _health.Restore(data.Health);

        OnExpChange?.Invoke(_exp, _progression.NeedExpFor(_level));
        OnGoldChange?.Invoke(_gold);
    }
}

[Serializable]
public class PlayerData
{
    public int Level;
    public int Exp;
    public int Gold;
    public HealthData Health;
}

[Serializable]
public class HealthData
{
    public float Health;
    public float MaxHealth;
}