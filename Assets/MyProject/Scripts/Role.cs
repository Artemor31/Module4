using UnityEngine;

[CreateAssetMenu(fileName = "Role", menuName = "Configs/Role", order = 0)]
public class Role : ScriptableObject
{
    public float Health;
    public Weapon Weapon;
}
