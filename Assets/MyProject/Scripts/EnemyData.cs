using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Configs/EnemyData")]
public class EnemyData : ScriptableObject
{
    public EnemyBrain Prefab;
    public float Health;
    public Weapon Weapon;
    public int Exp;
    public int Gold;
    public Item LootItem;
}