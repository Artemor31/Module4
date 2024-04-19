using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Button _close;
    [SerializeField] private List<SlotCell> _equip;
    [SerializeField] private List<Cell> _invent;
    private PlayerInventory _playerInventory;

    private void Start()
    {
        _close.onClick.AddListener(CloseClicked);
        _playerInventory = FindObjectOfType<Player>().Inventory;

        _playerInventory.OnInventoryChanged += OnInventoryChanged;
        _playerInventory.OnEquipmentChanged += OnEquipmentChanged;

        OnEquipmentChanged(_playerInventory.Equip);
        OnInventoryChanged(_playerInventory.Invent);

        foreach (var cell in _invent)
        {
            cell.OnCellClicked += OnCellClicked;
        }
    }

    private void OnEquipmentChanged(Dictionary<Slot, Item> data)
    {
        foreach (var item in _equip)
        {
            item.Cell.Clear();
        }

        foreach (var cell in _equip)
        {
            if (data.TryGetValue(cell.Slot, out var item))
            {
                cell.Cell.SetItem(item);
            }
        }
    }

    private void OnInventoryChanged(List<Item> data)
    {
        foreach (var item in _invent)
        {
            item.Clear();
        }

        for (int i = 0; i < data.Count; i++)
        {
            _invent[i].SetItem(data[i]);
        }
    }

    private void OnCellClicked(Item item) => _playerInventory.ForceEquip(item);
    private void CloseClicked() => gameObject.SetActive(false);
    private void OnDestroy() => _close.onClick.RemoveListener(CloseClicked);
}

[Serializable]
public class SlotCell
{
    public Slot Slot;
    public Cell Cell;
}

public enum Slot
{
    None = 0,
    Head = 1,
    Weapon = 2,
    Armor = 3,
    Boots = 4
}
