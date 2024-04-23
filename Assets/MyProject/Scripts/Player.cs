using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action<int> OnGoldChanged;
    public event Action<int, int> OnExpChanged;

    public PlayerInventory Inventory {  get; private set; }
    public int Level {  get; private set; }
    public int Exp {  get; private set; }
    public int Gold {  get; private set; }

    [SerializeField] private Health _health;
    [SerializeField] private Motion _mover;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private InputManager _input;
    [SerializeField] private LootPicker _lootPicker;
    private Progression _progression;

    public void Init(Role role, Camera camera, Progression progression)
    {
        _health.Init(role.Health);
        _input.Init(camera);
        _attacker.SetWeapon(role.Weapon);
        _lootPicker.OnLootPicked += OnLootPicked;
        _progression = progression;

        InitInventory(role);
    }

    public void BoostExp(int delta)
    {
        int needExp = _progression.ExpFor(Level) - Exp;

        if (needExp < delta)
        {
            Exp = delta - needExp;
            var healthBonus = _progression.HealthFor(Level);
            _health.BoostMaxHealth(healthBonus);
            Level++;
        }
        else
        {
            Exp += delta;
        }

        OnExpChanged?.Invoke(Exp, _progression.ExpFor(Level));
    }

    public void BoostGold(int delta)
    {
        Gold += delta;
        OnGoldChanged?.Invoke(Gold);
    }

    private void InitInventory(Role role)
    {
        Inventory = new PlayerInventory();
        Inventory.TryEquipItem(role.Weapon);
        Inventory.OnEquipmentChanged += OnEquipmentChanged;
    }

    private void OnEquipmentChanged(Dictionary<Slot, Item> equipment)
    {
        var weapon = equipment[Slot.Weapon] as Weapon;
        _attacker.SetWeapon(weapon);
    }

    private void OnLootPicked(Item item)
    {
        if (Inventory.TryEquipItem(item)) return;
        Inventory.TryAddItem(item);
    }

    void Update()
    {
        if (_health.IsDead) return;

        if (_input.Attacking)
        {
            _attacker.Attack();
        }

        if (!_attacker.IsAttacking)
        {
            _mover.Move(_input.Motion);
        }        
    }

    public PlayerData Save() => new PlayerData()
    {
        Level = Level,
        Exp = Exp,
        Gold = Gold,
        Health = _health.Save()
    };

    public void Restore(PlayerData data)
    {
        Level = data.Level;
        Exp = data.Exp;
        Gold = data.Gold;
        _health.Restore(data.Health);

        OnGoldChanged?.Invoke(Gold);
        OnExpChanged?.Invoke(Exp, _progression.ExpFor(Level));
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


