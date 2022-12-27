using Mirror;
using UnityEngine;

[RequireComponent(typeof(NetworkTransform), typeof(Rigidbody))]
public class MovablePlayer : BaseMirrorPlayer
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private GameObject _testObject;

    private BasePlayerMovement _surfaceMovement;
    private BasePlayerInput _movementInput;
    private BasePlayerCollisions _repulseColisions;

    private int _floorLayerMask = 6;    
    private bool _isOnGround;


    protected Rigidbody Rigidbody { get; private set; }

    protected override void Init()    
    {        
        Rigidbody = GetComponent<Rigidbody>();

        _surfaceMovement = new SurfacePlayerMovement(Rigidbody, _moveSpeed);
        _movementInput = new MovePlayerInput();
        _repulseColisions = new RepulsePlayerColisions(Rigidbody, this);        
    }

    protected override void Update()
    {
        if (isLocalPlayer)
        {
            base.Update();

            bool isMoveind = TryMove();
            bool isNotMovingOnGround = (isMoveind == false) && _isOnGround;

            if (isNotMovingOnGround)
            {
                Slowdown();
            }
        }     
    } 

    protected virtual void OnCollisionEnter(Collision collision)
    {              
        TrySetIsGrouded(collision, true);

        if (IsCanPlayerColision())
        {
            _repulseColisions?.CollisionWith(collision);
        }
    }    

    private void OnCollisionExit(Collision collision)
    {
        TrySetIsGrouded(collision, false);
    }


    private void TrySetIsGrouded(Collision collision, bool value)
    {
        if (collision.gameObject.layer == _floorLayerMask)
        {
            _isOnGround = value;
        }
    }

    protected virtual bool IsCanPlayerColision() => true;

    protected virtual bool TryMove()
    {
        if (IsCanMove(out Vector3 moveVector))
        {
            _surfaceMovement.Move(moveVector);
            return true;
        }
        return false;
    }    

    protected virtual bool IsCanMove(out Vector3 moveVector)
    {
        return _movementInput.IsInputExist(out moveVector) && _isOnGround;
    }

    private void Slowdown()
    {
        float slowDownCoeficient = 0.2f;

        Rigidbody.velocity = Vector3.MoveTowards(Rigidbody.velocity, Vector3.zero, slowDownCoeficient);        
        Rigidbody.angularVelocity = Vector3.MoveTowards(Rigidbody.angularVelocity, Vector3.zero, slowDownCoeficient);        
    }  

}


