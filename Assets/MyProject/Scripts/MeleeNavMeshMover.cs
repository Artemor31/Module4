using UnityEngine;
using UnityEngine.AI;

public class MeleeNavMeshMover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _range;

    Transform _player;

    private void Start()
    {
        _player = FindObjectOfType<Motion>().transform;
        _agent.SetDestination(_player.position);
    }

    private void Update()
    {
        _agent.SetDestination(_player.position);

        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);
        if ( distanceToPlayer < _range)
        {
            _agent.isStopped = true;
            _animator.SetFloat("Speed", 0);
        }
        else
        {
            _agent.isStopped = false;
            _animator.SetFloat("Speed", _rigidbody.velocity.magnitude);
        }
    }
}
