using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Motion _mover;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private InputManager _input;

    private void Start()
    {
        var role = StaticData.SelectedRole;
        _health.Init(role.Health);
        _attacker.Init(role.Weapon);
    }

    void Update()
    {
        if (_health.IsDead) return;

        if (_input.Attacking)
        {
            _attacker.Attack();
        }

        if (!_attacker.IsAttacking)
        {
            _mover.Move(_input.Motion);
        }        
    }
}
