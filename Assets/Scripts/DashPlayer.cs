using Mirror;
using UnityEngine;

public class DashPlayer : MovablePlayer
{
    [SerializeField] private float _dashDistance;
    [SerializeField] private float _dashForse;
    [SerializeField] private Vector2 _mouseSensetivity;

    [SerializeField] private Transform _cameraHolder;
    [SerializeField] private PhysicMaterial _dashPhysicMaterial;

    private BasePlayerInput _dashInput;
    private BasePlayerMovement _dashMovement;

    private BasePlayerInput _mouseMoveInput;
    private BasePlayerMovement _viewRotationMovement;

    private BasePlayerCollisions _apllyHitColision;
    private BasePlayerCollisions _getHitColision;

    [SyncVar] private int _score;
    [SyncVar] private bool _isInvulnerable;

    public bool IsDashing => _dashMovement.IsActive;
    public bool IsInvulnerable => _isInvulnerable;

    protected override void Init()
    {        
        base.Init();
        _dashInput = new DashPlayerInput();
        _dashMovement = new DashMovement(StartCoroutine, _dashDistance, _dashPhysicMaterial, Rigidbody, _dashForse);

        _mouseMoveInput = new MouseMoveInput();
        _viewRotationMovement = new MovementRotation(_cameraHolder, _mouseSensetivity, Rigidbody, 1);

        _apllyHitColision = new ApplyDashHitPlayerCollisions(this);

        _getHitColision = new GetDashHitPlayerCollisions(this);

        _apllyHitColision.CollisionСonfirm += OnApplyHitColisionConfirm;
        _getHitColision.CollisionСonfirm += OnGetHitCollisionСonfirm;

    }

    protected override void Update()
    {
        if (isLocalPlayer )
        {
            base.Update();

            if (_dashInput.IsInputExist(out Vector3 dashDirection))
            {
                _dashMovement.Move(dashDirection);
            }

            if (_mouseMoveInput.IsInputExist(out Vector3 mouseMovement))
            {
                _viewRotationMovement.Move(mouseMovement);
            }
        }         
    }

    private void OnDestroy()
    {
        _apllyHitColision.CollisionСonfirm -= OnApplyHitColisionConfirm;
        _getHitColision.CollisionСonfirm -= OnGetHitCollisionСonfirm;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        _apllyHitColision.CollisionWith(collision);
        _getHitColision.CollisionWith(collision);
    }

    protected override bool IsCanMove(out Vector3 moveVector)
    {
        return base.IsCanMove(out moveVector) && _dashMovement.IsActive == false;
    }

    [Command]
    private void OnApplyHitColisionConfirm()
    {
        _score++;
        Debug.Log(_score);
    }

    [Command]
    private void OnGetHitCollisionСonfirm()
    {
        _isInvulnerable = true;
        Invoke(nameof(ResetInvulnerable), 3f);
        Debug.Log(_isInvulnerable);
    }

    private void ResetInvulnerable()
    {
        _isInvulnerable = false;
    }

}





