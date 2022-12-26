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

    [SyncVar] public int health;

    protected override void Init()
    {        
        base.Init();
        _dashInput = new DashPlayerInput();
        _dashMovement = new DashMovement(StartCoroutine, _dashDistance, _dashPhysicMaterial, Rigidbody, _dashForse);

        _mouseMoveInput = new MouseMoveInput();
        _viewRotationMovement = new MovementRotation(_cameraHolder, _mouseSensetivity, Rigidbody, 1);
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
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        
    }

   

    protected override bool IsCanMove(out Vector3 moveVector)
    {
        return base.IsCanMove(out moveVector) && _dashMovement.IsActive == false;
    }

}





