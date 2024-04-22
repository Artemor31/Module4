using System;
using UnityEngine;
using UnityEngine.AI;

public class MeleeNavMeshMover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;

    public void Stop() => UpdateSpeed(0);

    public void MoveTo(Health target)
    {
        _agent.SetDestination(target.transform.position);
        UpdateSpeed(_rigidbody.velocity.magnitude);
    }

    public void UpdateSpeed(float speed)
    {
        if (speed <= 0.1f)
        {
            _agent.isStopped = true;
            _animator.SetFloat("Speed", speed);

        }
        else
        {
            _agent.isStopped = false;
            _animator.SetFloat("Speed", speed);
        }
    }

    public void LookAt(Health target) => transform.LookAt(target.transform.position);
}
