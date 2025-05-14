using UnityEngine;
using UnityEngine.Serialization;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float sensitivity;
    [SerializeField] private Transform playerCam;
    [SerializeField] private Transform orientation;
    private float _xMouse, _yMouse;
    private const float LookMultiplier = 0.01f;
    private float _xRot, _yRot;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (PlayerMovement.IsDead) return;

        (_xMouse, _yMouse) = PlayerHelper.GetMouseInputs();

        _yRot += _xMouse * sensitivity * LookMultiplier;
        _xRot -= _yMouse * sensitivity * LookMultiplier;
        _xRot = Mathf.Clamp(_xRot, -90f, 90f);

        playerCam.transform.rotation = Quaternion.Euler(_xRot, _yRot, 0f);
        orientation.transform.rotation = Quaternion.Euler(0f, _yRot, 0f);
    }
}