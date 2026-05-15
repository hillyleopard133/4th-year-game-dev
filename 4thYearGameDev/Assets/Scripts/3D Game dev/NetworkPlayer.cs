using Fusion;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    public override void Spawned()
    {
        // Only local player controls this camera
        if (Object.HasStateAuthority)
        {
            CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();

            if (cameraFollow != null)
            {
                cameraFollow.target = transform;
            }
        }
    }
}