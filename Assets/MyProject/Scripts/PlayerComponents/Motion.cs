using System;
using UnityEngine;
using UnityEngine.Windows;

public class Motion : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;
    private Camera _camera;

    private void Start() => _camera = Camera.main;

    public void Stop()
    {
        SetAnimatorSpeed(0);
    }

    public void Move(Vector3 input)
    {
        if (input.sqrMagnitude > 0.05f)
        {
            MoveTo(input);
            SetAnimatorSpeed(_controller.velocity.magnitude);
        }
        else
        {
            SetAnimatorSpeed(0);
        }
    }

    private void MoveTo(Vector3 input)
    {
        var direction = _camera.transform.TransformDirection(input);
        direction.y = 0;
        direction.Normalize();
        transform.forward = direction;
        direction += Physics.gravity;
        _controller.Move(direction * _speed * Time.deltaTime);
    }

    private void SetAnimatorSpeed(float speed) => _animator.SetFloat("Speed", speed);
}
