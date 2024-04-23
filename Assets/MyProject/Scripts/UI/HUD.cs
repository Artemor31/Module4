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

    public void Init(Player player)
    {
        _player = player;
        _player.GetComponent<Health>().OnHealthChange += OnHealthChange;
        _player.OnExpChanged += OnExpChanged;
        _player.OnGoldChanged += OnGoldChanged;
        _openInventory.onClick.AddListener(OpenInventoryClicked);
    }

    private void OnGoldChanged(int gold) => _gold.text = gold.ToString();
    private void OnHealthChange(Health health) => _healthValue.fillAmount = (float)health.CurrentHealth / health.MaxHealth;
    private void OnExpChanged(int exp, int nextLevelExp) => _expValue.fillAmount = (float)exp / nextLevelExp;
    private void OpenInventoryClicked() => _inventory.SetActive(true);

    private void OnDestroy()
    {
        _openInventory.onClick.RemoveListener(OpenInventoryClicked);
        _player.GetComponent<Health>().OnHealthChange -= OnHealthChange; 
        _player.OnExpChanged -= OnExpChanged;
        _player.OnGoldChanged -= OnGoldChanged;
    }
}
