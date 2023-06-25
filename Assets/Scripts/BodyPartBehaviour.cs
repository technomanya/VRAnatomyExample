using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PartState
{
    Opaque,
    Transparent,
    Disabled
}
public class BodyPartBehaviour : MonoBehaviour
{
    public PartState partState;

    [SerializeField] private Material _originalMaterial;
    [SerializeField] private Material _xRayMaterial;

    private MeshRenderer _rendererOwn;
    // Start is called before the first frame update
    void Start()
    {
        _xRayMaterial = GameManager.Instance._xRayMaterialGeneral;
        _rendererOwn = GetComponent<MeshRenderer>();
        _originalMaterial = _rendererOwn.material;

        partState = PartState.Opaque;  
    }

    private void ChangeState(PartState _state = PartState.Opaque,Material _xRayMat = null)
    {
        if (_state == PartState.Opaque)
        {
            _rendererOwn.material = _originalMaterial;
            partState = PartState.Opaque;
        }
        else if (_state == PartState.Transparent && _xRayMat)
        {
            _rendererOwn.material = _xRayMat;
            partState = PartState.Transparent;
        }
        else if(partState == PartState.Disabled)
        {
            _rendererOwn.enabled = false;
            partState = PartState.Disabled;
        }
    }

    public void ClickedOn()
    {
        if(partState == PartState.Opaque)
        {
            ChangeState(PartState.Transparent,_xRayMaterial);
        }
        else if (partState == PartState.Transparent)
        {
            ChangeState(PartState.Opaque);
        }
    }

    public void PartEnable(bool _enable)
    {
        if(_enable)
            ChangeState(PartState.Opaque);
        else
            ChangeState(PartState.Disabled);
    }
}
