using Fusion;
using UnityEngine;

public class PlayerStats : NetworkBehaviour
{
    [Networked]
    public int Health { get; set; }

    [Networked]
    public NetworkString<_16> PlayerName { get; set; }

    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            Health = 100;
            PlayerName = $"Player_{Runner.LocalPlayer.PlayerId}";
        }
    }
}