using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Configs/Weapon", order = 0)]
public class Weapon : Item
{
    public float AttackCooldown;
    public float Damage;
    public float Range;
    public AnimatorOverrideController OverrideController;
    public GripType Grip;
    public Projectile ProjectilePrefab;
}

public enum GripType
{
    None = 0,
    Left = 1,
    Right = 2
}
