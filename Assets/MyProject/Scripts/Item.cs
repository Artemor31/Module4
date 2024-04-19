using UnityEngine;

public abstract class Item : ScriptableObject
{
    public Slot Slot;
    public Sprite Icon;
    public GameObject Prefab;
}
