using UnityEngine;

public class Motion : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;
    private Camera _camera;

    private void Start() => _camera = Camera.main;

    void Update()
    {
        if (_input.Motion.sqrMagnitude > 0.05f )
        {
            var direction = _camera.transform.TransformDirection(_input.Motion);
            direction.y = 0;
            direction.Normalize();

            transform.forward = direction;
            direction += Physics.gravity;

            _controller.Move(direction * _speed * Time.deltaTime);
            _animator.SetFloat("Speed", _controller.velocity.magnitude);
        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }
    }
}
