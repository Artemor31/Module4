using UnityEngine;

public class Loot : MonoBehaviour
{
    public Item Item { get; private set; }

    public void Init(Item item)
    {
        Item = item;
        Instantiate(item.Prefab, transform.position, Quaternion.identity, transform);
    }

    public Item Collect()
    {
        Destroy(gameObject);
        return Item;
    }
}
