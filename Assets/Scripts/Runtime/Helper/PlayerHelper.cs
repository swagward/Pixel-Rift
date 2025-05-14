using UnityEngine;

public static class PlayerHelper
{
    public static (float hInput, float vInput) GetWASDInputs()
        => (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    public static (float mouseX, float mouseY) GetMouseInputs()
        => (Input.GetAxisRaw("Mouse X"), 
            Input.GetAxisRaw("Mouse Y"));

    public static bool GroundCheck(Transform player, LayerMask groundLayer, float radius)
        => Physics.CheckSphere(player.position - new Vector3(0, 1 ,0), radius, groundLayer);
}