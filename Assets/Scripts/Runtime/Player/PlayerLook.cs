using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float sensitivity;
    public Transform orientation;
    private float _xRot, _yRot;
    

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        var (mouseX, mouseY) = PlayerHelper.GetMouseInputs(sensitivity);

        _yRot += mouseX;
        _xRot -= mouseY;
        _xRot = Mathf.Clamp(_xRot, -90f, 90f);
        
        transform.rotation = Quaternion.Euler(_xRot, _yRot, 0f);
        orientation.rotation = Quaternion.Euler(0f, _yRot, 0f);
    }
}
