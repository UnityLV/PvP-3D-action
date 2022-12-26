using UnityEngine;

public class MovePlayerColisions : BasePlayerCollisions
{
    protected Rigidbody Rigidbody { get; private set; }

    public MovePlayerColisions(Rigidbody rigidbody,MovablePlayer thisPlayer) : base(thisPlayer)
    {
        Rigidbody = rigidbody;
    }

    public override void CollisonWith(Collision colision)
    {
        TryRepulse(colision);
    }

    private void TryRepulse(Collision colision)
    {
        if (colision.gameObject.TryGetComponent(out Rigidbody rigidbody))
        {
            Repulse(rigidbody);
        }
    }

    private void Repulse(Rigidbody otherRigidbody)
    {
        (Vector3 newVelocity1, Vector3 newVelocity2) = CalculateNewVelocities(
            Rigidbody.velocity, otherRigidbody.velocity, Rigidbody.mass, otherRigidbody.mass);

        SetNewVelosities(otherRigidbody, newVelocity1, newVelocity2);
    }

    private (Vector3, Vector3) CalculateNewVelocities(Vector3 velocityA, Vector3 velocityB, float massA, float massB)
    {
        Vector3 newVelocity1 = (massA * velocityA + massB * velocityB) / (massA + massB);
        Vector3 newVelocity2 = (massB * velocityB + massA * velocityA) / (massA + massB);
        return (newVelocity1, newVelocity2);
    }

    private void SetNewVelosities(Rigidbody otherRigidbody, Vector3 newVelocity1, Vector3 newVelocity2)
    {
        Rigidbody.velocity = newVelocity1;
        otherRigidbody.velocity = newVelocity2;
    }

}


