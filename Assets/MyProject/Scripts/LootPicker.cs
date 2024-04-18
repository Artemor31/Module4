using System;
using UnityEngine;

public class LootPicker : MonoBehaviour
{
    public event Action<Item> OnItemPicked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Loot>(out var loot))
        {
            var item = loot.Collect();
            OnItemPicked?.Invoke(item);
        }
    }
}
