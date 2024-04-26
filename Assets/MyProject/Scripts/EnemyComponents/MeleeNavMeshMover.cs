public class MeleeNavMeshMover : NavMeshMover
{
    public override void MoveTo(Health player)
    {
        _agent.SetDestination(player.transform.position);
        UpdateSpeed(_agent.speed);
    }
}
