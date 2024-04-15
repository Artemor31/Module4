using System;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Button _openInventory;
    [SerializeField] private GameObject _inventory;
    [SerializeField] private Image _healthValue;
    private Health _player;

    private void OnEnable()
    {
        _openInventory.onClick.AddListener(OpenInventoryClicked);
    }

    private void Start()
    {
        _player = FindObjectOfType<Motion>().GetComponent<Health>();
        _player.OnHealthChange += OnHealthChange;
    }

    private void OnHealthChange(Health health)
    {
        _healthValue.fillAmount = (float)health.CurrentHealth / health.MaxHealth;
    }

    private void OpenInventoryClicked()
    {
        _inventory.SetActive(true);
    }

    private void OnDisable()
    {
        _openInventory.onClick.RemoveListener(OpenInventoryClicked);
        _player.OnHealthChange -= OnHealthChange;
    }
}
