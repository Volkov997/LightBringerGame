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
    private Vector3 _forces;

    private PlayerInput _playerInput;
    private Vector2 _moveInput;
    private Vector2 _lookInput;
    private Vector3 _movedir;

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
    public void OnJump(InputValue value){}

    // Update is called once per frame
    void Update()
    {
        _forces = Vector3.zero;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        HandleMovement();
    }

    void HandleMovement()
    {
        var dT = Time.deltaTime;

        if (!_cc.isGrounded)
        {
            _forces += Physics.gravity;
        }

        //look
        _xRotation = _lookInput.x * _sensitivity * dT;
        _yRotation = _lookInput.y * _sensitivity * dT;
        _yRotation = Mathf.Clamp(_yRotation, -80f, 80f);

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
        _forces += new Vector3(dV.x,0f,dV.z) * (_cc.isGrounded ? 18f : 5f);
        //drag force
        _velocity += _forces * dT;
        _cc.Move(_velocity * dT);
    }
}
