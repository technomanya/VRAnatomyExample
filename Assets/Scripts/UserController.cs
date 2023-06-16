using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    public float cameraMoveSpeed;
    public float mouseSensitivity = 100f;

    private float mouseX;
    private float mouseY;
    private float xRotation = 0f;

    private float _inputLeftRight, _inputForwardBack, _inputUpDown;

    [SerializeField] private Camera _userCamera;
    [SerializeField] private Transform _moveTargetTransform;
    [SerializeField] private int _camMoveInputMultiplayer;

    void Start()
    {
        if (!_userCamera)
            _userCamera = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
            _inputUpDown = 1.0f*_camMoveInputMultiplayer;
        else if (Input.GetKey(KeyCode.E))
            _inputUpDown = -1.0f* _camMoveInputMultiplayer;
        else
            _inputUpDown = 0;

        _inputLeftRight = Input.GetAxis("Horizontal")* _camMoveInputMultiplayer;
        _inputForwardBack = Input.GetAxis("Vertical")* _camMoveInputMultiplayer;

        if(_inputForwardBack != 0 || _inputLeftRight != 0 || _inputUpDown != 0)
        {
            _moveTargetTransform.localPosition = Vector3.forward * _inputForwardBack + Vector3.up * _inputUpDown + Vector3.right * _inputLeftRight;
        }
        else
        {
            _moveTargetTransform.localPosition = Vector3.zero;
        }

        MoveCharacter(_moveTargetTransform.position);
    }

    private void LateUpdate()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.parent.Rotate(Vector3.up * mouseX);
    }

    public void MoveCharacter(Vector3 _target)
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, Time.deltaTime * cameraMoveSpeed);
    }
}
