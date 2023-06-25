using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Material _xRayMaterialGeneral;

    [SerializeField] private List<GameObject> _bodyPartsGroup;
    private Dictionary<string, GameObject> _bodyPartGroupDict = new Dictionary<string, GameObject>();
    private GameObject _tempBodyGroup;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var part in _bodyPartsGroup)
        {
            _bodyPartGroupDict.Add(part.name, part);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BodyGroupMakeTransparent(string _name)
    {
        _bodyPartGroupDict.TryGetValue(_name,out _tempBodyGroup);
        if (_tempBodyGroup != null)
        {
            ClickOnPartsInChildren();
        }
    }

    public void BodyGroupEnable(string _name)
    {
        _bodyPartGroupDict.TryGetValue(_name, out _tempBodyGroup);
        ClickOnPartsInChildren();
        if (_tempBodyGroup != null)
        {
            _tempBodyGroup.SetActive(true);
        }
    }

    public void BodyGroupDisable(string _name)
    {
        _bodyPartGroupDict.TryGetValue(_name, out _tempBodyGroup);
        ClickOnPartsInChildren();
        if (_tempBodyGroup != null)
        {
            _tempBodyGroup.SetActive(false);
        }
    }

    private void ClickOnPartsInChildren()
    {
        var bodyPartList = _tempBodyGroup.GetComponentsInChildren<BodyPartBehaviour>();

        foreach (var part in bodyPartList)
        {
            part.ClickedOn();
        }
    }
}
