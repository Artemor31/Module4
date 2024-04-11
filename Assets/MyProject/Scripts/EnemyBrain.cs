using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private MeleeNavMeshMover _mover;
    [SerializeField] private EnemyAttacker _attacker;

    private Health _player;

    private void Start() => _player = FindObjectOfType<Motion>().GetComponent<Health>();

    private void Update()
    {
        if (_player.IsDead) return;

        if (_attacker.TargetInRange(_player))
        {
            _mover.Stop();
            _attacker.Attack();
        }
        else 
        {
            _mover.MoveTo(_player);
        }
    }
}
