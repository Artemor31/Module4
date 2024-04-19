using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerInventory Inventory {  get; private set; }
    [SerializeField] private Health _health;
    [SerializeField] private Motion _mover;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private InputManager _input;
    [SerializeField] private LootPicker _lootPicker;

    private void Start()
    {
        var role = StaticData.SelectedRole;
        _health.Init(role.Health);
        _attacker.SetWeapon(role.Weapon);
        _lootPicker.OnLootPicked += OnLootPicked;

        InitInventory(role);
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
}
