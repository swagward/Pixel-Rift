using UnityEngine;

public static class MovementHelper
{
    public static (float hInput, float vInput) GetInputs()
        => (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    public static bool GroundCheck(Transform player, LayerMask groundLayer, float playerHeight)
        => Physics.Raycast(player.position, Vector3.down, playerHeight / 2 + 0.5f, groundLayer);
}