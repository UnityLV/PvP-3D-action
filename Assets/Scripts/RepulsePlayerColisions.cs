using UnityEngine;

public class RepulsePlayerColisions : BasePlayerCollisions
{
    protected Rigidbody Rigidbody { get; private set; }
    private float _scaler = 14f;

    public RepulsePlayerColisions(Rigidbody rigidbody, MovablePlayer thisPlayer) : base(thisPlayer)
    {
        Rigidbody = rigidbody;
    }

    public override void CollisonWith(Collision collision)
    {
        Debug.Log(collision.gameObject.TryGetComponent(out MovablePlayer mova2blePlayer));
        if (collision.gameObject.TryGetComponent(out MovablePlayer movablePlayer))
        {
            Repulse(collision);
            InvokeColisionConfirm();
        }
    }

    private void Repulse(Collision collision)
    {
        Vector3 contactPoint = collision.GetContact(0).point;
        Vector3 repulseDirection = ThisPlayer.transform.position - contactPoint;
        float? hitForse = Rigidbody.velocity.magnitude + collision.rigidbody?.velocity.magnitude;

        Rigidbody.velocity = Vector3.zero;

        Vector3 repulseVector = repulseDirection * (hitForse + _scaler ?? _scaler);
        Rigidbody.AddForce(repulseVector, ForceMode.Impulse);
    }

}


