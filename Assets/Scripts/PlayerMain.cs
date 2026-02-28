using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerMain : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private float illuminated = 0f;

    [SerializeField]
    private float sanity = 100f;

    private CharacterController _cc;
    private Vector3 _velocity;
    private Vector3 _accel = Vector3.zero;

    private PlayerInput _playerInput;
    private Vector2 _moveInput;
    private Vector2 _lookInput;
    private Vector3 _movedir;
    private bool _jump;

    private Camera _cam;
    private float _sensitivity = 30f;
    private float _xRotation = 0f;
    private float _yRotation = 0f;

    void Awake()
    {
        _cc = GetComponent<CharacterController>();
        _cam = GetComponentInChildren<Camera>();
        _playerInput = GetComponent<PlayerInput>();
    }
    void Start()
    {
    }

    void FixedUpdate()
    {
    }
    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }
    public void OnLook(InputValue value){
        _lookInput += value.Get<Vector2>();
    }
    public void OnAttack(InputValue value){}
    public void OnInteract(InputValue value){}
    public void OnCrouch(InputValue value){}
    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        HandleMovement();

        _accel = Vector3.zero;
    }

    void HandleMovement()
    {
        var dT = Time.deltaTime;

        if (!_cc.isGrounded)
        {
            _accel += Physics.gravity;
        } else if(_playerInput.actions["Jump"].IsPressed()){
            //impulse of 0.6s of gravity
            _velocity.y = -Physics.gravity.y * 0.6f;
        }

        //look
        _xRotation = Mathf.Lerp(_xRotation, _lookInput.x * _sensitivity * dT, 0.2f);
        _yRotation = Mathf.Lerp(_yRotation, _lookInput.y * _sensitivity * dT, 0.2f);

        var signedYAngle = (_cam.transform.localEulerAngles.x + 180f) % 360f - 180f;
        if ((_yRotation >= 0 || signedYAngle <= 80f) && (_yRotation < 0 || signedYAngle >= -80f))
        {
            _cam.transform.Rotate(Vector3.right * -_yRotation, Space.Self);
        }
        transform.Rotate(Vector3.up * _xRotation);
        
        _lookInput = Vector2.zero;

        //movement
        Vector3 targetMovedir = new Vector3(_moveInput.x, 0, _moveInput.y);
        targetMovedir = transform.TransformDirection(targetMovedir);
     
        var sprinting = _playerInput.actions["Sprint"].IsPressed();
        moveSpeed = sprinting ? 10f : 5f;
        //movement work
        var dV = (targetMovedir * moveSpeed - _velocity);
        _accel += new Vector3(dV.x,0f,dV.z) * (_cc.isGrounded ? 18f : 5f);
        _velocity += _accel * dT;
        _cc.Move(_velocity * dT);
    }
}
