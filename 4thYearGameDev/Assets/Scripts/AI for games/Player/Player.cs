using UnityEngine;

public class Player : Singleton<Player>
{
    private PlayerAnimations animations;

    protected override void Awake()
    {
        base.Awake();
        animations = GetComponent<PlayerAnimations>();
    }

}
