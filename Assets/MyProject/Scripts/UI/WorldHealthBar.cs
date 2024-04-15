using UnityEngine;
using UnityEngine.UI;

public class WorldHealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthValue;
    [SerializeField] private Health _health;
    private Camera _camera;

    private void OnEnable()
    {
        _health.OnHealthChange += OnHealthChange;
        _camera = Camera.main;
    }

    private void Update()
    {
        transform.LookAt(_camera.transform);
    }

    private void OnDisable()
    {

        _health.OnHealthChange -= OnHealthChange;
    }

    private void OnHealthChange(Health health)
    {
        _healthValue.fillAmount = (float)health.CurrentHealth / (float)health.MaxHealth;
    }
}
