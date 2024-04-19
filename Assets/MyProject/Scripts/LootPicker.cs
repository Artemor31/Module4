using System;
using UnityEngine;

public class LootPicker : MonoBehaviour
{
    public event Action<Item> OnLootPicked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Loot>(out var loot))
        {
            var item = loot.Collect();
            OnLootPicked?.Invoke(item);
        }
    }
}
