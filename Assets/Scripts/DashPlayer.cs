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
    private int _score;

    public bool IsDashing => _dashMovement.IsActive;

    public bool IsInvulnerable { get; private set; }

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
    }

    protected override bool IsCanMove(out Vector3 moveVector)
    {
        return base.IsCanMove(out moveVector) && _dashMovement.IsActive == false;
    }

    private void OnApplyHitColisionConfirm()
    {
        _score++;
    }

    private void OnGetHitCollisionСonfirm()
    {
        IsInvulnerable = true;
    }

}





