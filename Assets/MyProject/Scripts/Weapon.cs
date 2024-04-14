using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Configs/Weapon", order = 0)]
public class Weapon : ScriptableObject
{
    public float AttackCooldown;
    public float Damage;
    public float Range;
    public GameObject Prefab;
    public AnimatorOverrideController OverrideController;
}
