using UnityEngine;
using UnityEngine.AI;

public abstract class NavMeshMover : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent _agent;
    [SerializeField] protected Animator _animator;
    public abstract void MoveTo(Health player);
    public void LookAt() => transform.LookAt(_agent.destination);
    public void Stop() => UpdateSpeed(0);
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
}
