using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed;
    private float _damage;
    public void Init(float speed, float damage)
    {
        _speed = speed;
        _damage = damage;
    }

    private void Update() => transform.Translate(_speed * Time.deltaTime * Vector3.forward);

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Health>(out var health))
        {
            health.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
