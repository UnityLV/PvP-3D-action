public class DashPlayer : MovablePlayer
{
    private float _dashSpeed;
    private DashPlayerInput _playerInput;
    private DashMovement _dashMovement;

    private void Awake()
    {
        _playerInput = new();
        _dashMovement = new( Rigidbody, _dashSpeed,StartCoroutine);
    }

    protected override void Update()
    {
        base.Update();

        if (_playerInput.IsJerkTrigger())
        {

        }
    }
}


