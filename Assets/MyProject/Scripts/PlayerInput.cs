using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool Attacking => Input.GetMouseButtonDown(0);
    public Vector3 Motion => new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
}
