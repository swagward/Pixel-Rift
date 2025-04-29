using UnityEngine;

public static class PlayerHelper
{
    public static (float hInput, float vInput) GetWASDInputs()
        => (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    public static (float mouseX, float mouseY) GetMouseInputs(float mouseSensitivity)
        => (Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSensitivity, 
            Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSensitivity);

    public static bool GroundCheck(Transform player, LayerMask groundLayer, float playerHeight)
        => Physics.Raycast(player.position, Vector3.down, (playerHeight * 0.5f) + 0.5f, groundLayer);
}