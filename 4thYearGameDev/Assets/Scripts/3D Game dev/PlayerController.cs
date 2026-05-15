using Fusion;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f;

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority)
            return;

        float h = 0;
        float v = 0;

        if (Input.GetKey(KeyCode.W)) v = 1;
        if (Input.GetKey(KeyCode.S)) v = -1;
        if (Input.GetKey(KeyCode.A)) h = -1;
        if (Input.GetKey(KeyCode.D)) h = 1;

        Vector3 inputDir = new Vector3(h, 0, v).normalized;

        if (inputDir.magnitude == 0)
            return;

        // Camera-relative direction
        Transform cam = Camera.main.transform;

        Vector3 camForward = cam.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cam.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 moveDir = camForward * inputDir.z + camRight * inputDir.x;

        // Move player
        transform.position += moveDir * speed * Runner.DeltaTime;

        // Rotate player to face movement direction
        Quaternion targetRot = Quaternion.LookRotation(moveDir);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            rotationSpeed * Runner.DeltaTime
        );
    }
}