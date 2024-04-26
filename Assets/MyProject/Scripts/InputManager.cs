using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    private RaycastHit[] _hits = new RaycastHit[1];
    private Camera _camera;

    public bool Casting => Input.GetMouseButtonDown(1);
    public bool Attacking => Input.GetMouseButtonDown(0);
    public Vector3 Motion => new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

    public void Init(Camera camera)
    {
        _camera = camera;
    }

    public Vector3 GetPointerPosition()
    {
        var mouse = Input.mousePosition;
        Ray ray = _camera.ScreenPointToRay(mouse);
        Physics.RaycastNonAlloc(ray, _hits, 100, _layerMask);
        return _hits[0].point;
    }
}
