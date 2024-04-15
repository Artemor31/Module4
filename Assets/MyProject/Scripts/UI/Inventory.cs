using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Button _close;

    private void OnEnable()
    {
        _close.onClick.AddListener(CloseClicked);
    }

    private void CloseClicked()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _close.onClick.RemoveListener(CloseClicked);
    }
}
