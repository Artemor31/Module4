using System;
using System.Collections.Generic;

public class PlayerInventory
{
    private const int MaxInventSlots = 6;

    public event Action<Dictionary<Slot, Item>> OnEquipmentChanged;
    public event Action<List<Item>> OnInventoryChanged;
    public Dictionary<Slot, Item> Equip { get; private set; }
    public List<Item> Invent { get; private set; }

    public PlayerInventory()
    {
        Invent = new List<Item>();
        Equip = new Dictionary<Slot, Item>();
    }

    public bool TryAddItem(Item item)
    {
        if (Invent.Count >= MaxInventSlots) return false;

        Invent.Add(item);
        OnInventoryChanged?.Invoke(Invent);
        return true;
    }

    public bool TryEquipItem(Item item)
    {
        if (Equip.TryGetValue(item.Slot, out var equiped)) return false;

        Equip[item.Slot] = item;
        OnEquipmentChanged?.Invoke(Equip);
        return true;
    }

    public void ForceEquip(Item item)
    {
        if (Equip.TryGetValue(item.Slot, out var equiped))
        {
            Invent.Remove(item);
            Invent.Add(equiped);
            Equip[item.Slot] = item;

            OnEquipmentChanged?.Invoke(Equip);
            OnInventoryChanged?.Invoke(Invent);
        }
        else
        {
            Equip[item.Slot] = item;
            OnEquipmentChanged?.Invoke(Equip);
        }
    }
}
