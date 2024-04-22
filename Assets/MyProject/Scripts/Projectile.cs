using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _damage;

    public void Init(float damage)
    {
        _damage = damage;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Health>(out var health))
        {
            health.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
