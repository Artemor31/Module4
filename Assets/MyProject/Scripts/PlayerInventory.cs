using System;
using System.Collections.Generic;

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
