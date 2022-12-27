using UnityEngine;

public class GetDashHitPlayerCollisions : BasePlayerCollisions
{
    private new DashPlayer ThisPlayer;
    public GetDashHitPlayerCollisions(DashPlayer thisPlayer) : base(thisPlayer)
    {
        ThisPlayer = thisPlayer;
    }

    public override void CollisionWith(Collision Collision)
    {
        bool isColisionWithAnotherDashPlayer = Collision.gameObject.TryGetComponent(out DashPlayer anotherDashPlayer);

        bool isGetHitByAnotherPlayer = isColisionWithAnotherDashPlayer &&
            ThisPlayer.IsInvulnerable == false &&
            anotherDashPlayer.IsDashing;

        if (isGetHitByAnotherPlayer)
        {
            InvokeColisionConfirm();
        }
    }

}


