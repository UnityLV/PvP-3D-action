using UnityEngine;

public class ApplyDashHitPlayerCollisions : BasePlayerCollisions
{
    private new DashPlayer ThisPlayer;
    public ApplyDashHitPlayerCollisions(DashPlayer thisPlayer) : base(thisPlayer)
    {
        ThisPlayer = thisPlayer;
    }

    public override void CollisionWith(Collision Collision)
    {
        bool isColisionWithAnotherDashPlayer = Collision.gameObject.TryGetComponent(out DashPlayer anotherDashPlayer);

        bool isWeDashingHitAnotherPlayer = isColisionWithAnotherDashPlayer && 
            ThisPlayer.IsDashing && 
            anotherDashPlayer.IsInvulnerable == false;

        if (isWeDashingHitAnotherPlayer)
        {
            InvokeColisionConfirm();            
        }
    }

}


