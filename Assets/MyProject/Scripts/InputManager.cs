using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;
    private RaycastHit[] _hits = new RaycastHit[1];
    private Camera _camera;

    public void Init(Camera camera) => _camera = camera;
    public bool Attacking => Input.GetMouseButtonDown(0);
    public Vector3 Motion => new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

    public bool GetClickPoint(out Vector3 point)
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.RaycastNonAlloc(ray, _hits, 1000, _groundMask) > 0)
        {
            point = _hits[0].point;
            return true;
        }

        point = Vector3.zero;
        return false;
    }
}
