using UnityEngine;

public class Loot : MonoBehaviour
{
    private Item _item;

    public void Init(Item item)
    {
        _item = item;
        Instantiate(_item.Prefab, transform.position, Quaternion.identity, transform);
    }

    public Item Collect()
    {
        Destroy(gameObject);
        return _item;
    }
}
