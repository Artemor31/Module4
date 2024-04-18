using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Button _close;
    [SerializeField] private List<EquipmentCell> _equipment;
    [SerializeField] private List<Cell> _inventory;

    private Player _player;

    private void Start()
    {
        _close.onClick.AddListener(CloseClicked);
        _player = FindObjectOfType<Player>();

        SetupEquipment(_player.playerInventory.Equipment);
        _player.playerInventory.OnEquipmentChanged += SetupEquipment;
        _player.playerInventory.OnInventoryChanged += SetupInventory;

        foreach (Cell cell in _inventory)
        {
            cell.OnClicked += CellOnClicked;
        }
    }

    private void CellOnClicked(Item item)
    {

        //invent.RemoveFromInvent(item);
        //invent.RemoveEquip(item);
        //invent.AddEquip(item);

        //SetupInventory(invent.Inventory);
        //SetupEquipment(invent.Equipment);
    }

    private void SetupInventory(List<Item> list)
    {
        foreach (var cell in _inventory)
        {
            cell.RemoveItem();
        }

        for (int i = 0; i < list.Count; i++)
        {
            _inventory[i].SetItem(list[i]);
        }
    }

    private void SetupEquipment(Dictionary<Slot, Item> equipment)
    {
        foreach (var cell in _equipment)
        {
            cell.Cell.RemoveItem();
        }

        foreach (var cell in _equipment)
        {
            if (equipment.TryGetValue(cell.Slot, out var item))
            {
                cell.Cell.SetItem(item);
            }
        }
    }

    private void CloseClicked()
    {
        gameObject.SetActive(false);
    }
}

[Serializable]
public class EquipmentCell
{
    public Slot Slot;
    public Cell Cell;
}
