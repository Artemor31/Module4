using UnityEngine;

public class RangeNavMeshMover : NavMeshMover
{
    private float _range;

    public void Init(float range) => _range = range;

    private Vector3 FindRandomPoint(Vector3 playerPosition)
    {
        int counter = 5;
        Vector3 position;
        do
        {
            Vector2 point = Random.insideUnitCircle;

            if (Mathf.Abs(point.x) < 0.5f)
            {
                point.x *= 2;
            }

            if (Mathf.Abs(point.y) < 0.5f)
            {
                point.y *= 2;
            }

            position = new Vector3(point.x, 0, point.y) * _range;
            position += playerPosition;
            counter--;
        }
        while (!_agent.Raycast(position, out var hit) && counter > 0);

        return position;
    }

    public override void MoveTo(Health player)
    {
        Vector3 point = FindRandomPoint(player.transform.position);
        _agent.SetDestination(point);
        UpdateSpeed(_agent.speed);
    }
}