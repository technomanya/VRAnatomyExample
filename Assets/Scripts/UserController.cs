using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    public float cameraMoveSpeed;

    private float _inputLeftRight, _inputForwardBack, _inputUpDown;

    [SerializeField] private Camera _userCamera;
    [SerializeField] private Transform _moveTargetTransform;
    [SerializeField] private float _camMoveInputMultiplayer;

    void Start()
    {
        if (!_userCamera)
            _userCamera = Camera.main;
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

        MoveCharacter(transform,_moveTargetTransform.position);
    }

    public void MoveCharacter(Transform _movingObject,Vector3 _target)
    {
        transform.position = Vector3.MoveTowards(_movingObject.position, _target, Time.deltaTime * cameraMoveSpeed);
    }
}
