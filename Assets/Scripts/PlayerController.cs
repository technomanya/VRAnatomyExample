using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float cameraMoveSpeed;

    private float _inputLeftRight, _inputForwardBack, _inputUpDown;

    private PointerEventData pointerEventData = new PointerEventData(EventSystem.current);

    [SerializeField] private Camera _userCamera;
    [SerializeField] private Transform _moveTargetTransform;
    [SerializeField] private float _camMoveInputMultiplayer;

    [SerializeField] private Image cursor;
    [SerializeField] private LayerMask uI;

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

        if (Input.GetButtonDown("Fire1"))
        {
            PerformRaycast();
        }
    }

    public void MoveCharacter(Transform _movingObject,Vector3 _target)
    {
        transform.position = Vector3.MoveTowards(_movingObject.position, _target, Time.deltaTime * cameraMoveSpeed);
    }

    private void PerformRaycast()
    {
        Ray ray = _userCamera.ScreenPointToRay(cursor.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, uI))
        {
            var bodyPart = hit.collider.GetComponent<BodyPartBehaviour>();
            var button = hit.collider.GetComponent<IPointerClickHandler>(); ;
            if (bodyPart)
            {
                bodyPart.ClickedOn();
            }

            if(button!=null)
            {
                button.OnPointerClick(pointerEventData);
            }
        }
    }
}
