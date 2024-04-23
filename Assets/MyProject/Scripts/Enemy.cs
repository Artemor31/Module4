using UnityEngine;


[CreateAssetMenu(fileName = "Enemy", menuName = "Configs/Enemy", order = 0)]
public class Enemy : ScriptableObject
{
    public EnemyBrain Prefab;
    public Weapon Weapon;
    public float StartHealth;
    public int Gold;
    public int Exp;
    public Item Loot;
}
