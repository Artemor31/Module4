using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
    public event Action<Dictionary<Slot, Item>> OnEquipmentChanged;
    public event Action<List<Item>> OnInventoryChanged;

    public Dictionary<Slot, Item> Equipment {  get; private set; }
    public List<Item> Inventory { get; private set; }

    public PlayerInventory()
    {
        Equipment = new Dictionary<Slot, Item>();
        Inventory = new List<Item>();
    }

    public void AddEquip(Slot slot, Item item)
    {
        Equipment.Add(slot, item);
        OnEquipmentChanged?.Invoke(Equipment);
    }

    public void AddInvent(Item item)
    {
        Inventory.Add(item);
        OnInventoryChanged?.Invoke(Inventory);
    }

    internal void RemoveFromInvent(Item item)
    {
        throw new NotImplementedException();
    }
}

public class Player : MonoBehaviour
{
    public PlayerInventory playerInventory { get; private set; }

    [SerializeField] private Health _health;
    [SerializeField] private Motion _mover;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private InputManager _input;
    [SerializeField] private LootPicker _lootPicker;
    [SerializeField] private Caster _caster;

    private void Awake()
    {
        var role = StaticData.SelectedRole;
        InitComponents(role);
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
            _caster.CastSpell(_health);
        }

        if (!_attacker.IsAttacking)
        {
            _mover.Move(_input.Motion);
        }        
    }
}
