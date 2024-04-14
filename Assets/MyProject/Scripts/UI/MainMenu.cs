using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startGame;
    [SerializeField] private List<RoleButtonData> _roleButtons;

    private RoleButtonData _selected;

    private void OnEnable()
    {
        _startGame.onClick.AddListener(StartClicked);
        foreach (var button in _roleButtons)
        {
            button.Init();
            button.ButtonClicked += ButtonClicked;
        }
    }

    private void OnDisable()
    {
        _startGame.onClick.RemoveListener(StartClicked);
        foreach (var button in _roleButtons)
        {
            button.UnInit();
            button.ButtonClicked -= ButtonClicked;
        }
    }

    private void ButtonClicked(RoleButtonData data)
    {
        _selected = data;

        foreach (var roleButton in _roleButtons)
        {
            roleButton.Button.GetComponent<Image>().color = Color.gray;
        }
        _selected.Button.GetComponent<Image>().color = Color.white;
    }

    private void StartClicked()
    {
        if (_selected != null)
        {
            SceneManager.LoadScene(0);
            StaticData.SelectedRole = _selected.Role;
        }
    }
}

[Serializable]
public class RoleButtonData
{
    public event Action<RoleButtonData> ButtonClicked;
    public Button Button;
    public Role Role;

    public void Init() => Button.onClick.AddListener(Clicked);
    public void UnInit() => Button.onClick.RemoveListener(Clicked);
    private void Clicked() => ButtonClicked?.Invoke(this);
}
