using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Button _openInventory;
    [SerializeField] private GameObject _inventory;
    [SerializeField] private Image _healthValue;
    [SerializeField] private Image _expValue;
    [SerializeField] private TextMeshProUGUI _gold;
    private Player _player;

    internal void Init(Player player)
    {
        _player = player;
        _player.GetComponent<Health>().OnHealthChange += OnHealthChange;
        _player.OnExpChange += OnExpChange;
        _player.OnGoldChange += OnGoldChange;

        _openInventory.onClick.AddListener(OpenInventoryClicked);
    }

    private void OnGoldChange(int gold) => _gold.text = gold.ToString();

    private void OnExpChange(int exp, int maxExp) => _expValue.fillAmount = (float)exp / maxExp;

    private void OnHealthChange(Health health) => _healthValue.fillAmount = (float)health.CurrentHealth / health.MaxHealth;

    private void OpenInventoryClicked() => _inventory.SetActive(true);

    private void OnDestroy()
    {
        _player.GetComponent<Health>().OnHealthChange -= OnHealthChange;
        _player.OnExpChange -= OnExpChange;
        _player.OnGoldChange -= OnGoldChange;

        _openInventory.onClick.RemoveListener(OpenInventoryClicked);
    }
}
