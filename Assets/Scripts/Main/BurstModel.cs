using System;
using System.Collections;
using System.Linq;

using UI;

using UnityEngine;

public class BurstModel : MonoBehaviour
{
    [SerializeField] private Transform _itemTransform;
    [SerializeField] private AnimationCurve _MovementCurve;
    [SerializeField] private MainUI _mainUI;
    
    private Transform[] _componentsTransforms;
    private Transform[] _componentsStartTransforms;
    
    private bool _bursted = false;
    private const float _step = 15f;
    private const float _offsetX = -50f;
    private const float TimeToMove = 1f;
    
    public delegate void Bursted (bool state);
    public event Bursted ThenBursted;
    
    private void Awake()
    {
        GetComponentsTransforms();
        SaveStartPositions();
        _mainUI.OnItemButtonPressed += ChangeBurstState;
    }
    private void ChangeBurstState()
    {
        if (_bursted == false)
        {
            _bursted = true;
            Burst(-1);
        }

        else
        {
            _bursted = false;
            Burst(1);
        }
    }
    private void SaveStartPositions()
    {
        _componentsStartTransforms = new Transform[_componentsTransforms.Length];
        Array.Copy(_componentsTransforms, _componentsStartTransforms, _componentsTransforms.Length);
    }
    private void GetComponentsTransforms()
    {
        var allComponentsWithParent = _itemTransform.GetComponentsInChildren<Transform>();

        _componentsTransforms = allComponentsWithParent.
            Where(ComponentTransform => ComponentTransform != _itemTransform.transform).ToArray();
    }

    private void Burst(int multiple)
    {
        for (var index = 0; index < _componentsTransforms.Length; index++)
        {
            StartCoroutine(MoveComponentCoroutine(index, multiple));
        }
    }

    private IEnumerator MoveComponentCoroutine(int index, int multiple, Action onCoroutineFinished = null)
    {
        float timeElapsed = 0;
        Vector3 startPosition = _componentsStartTransforms[index].position;
        float newX = startPosition.x + _offsetX * multiple + _step * index * multiple;
        Vector3 newPosition = new Vector3(newX, startPosition.y, startPosition.z);
        
        while (timeElapsed < TimeToMove)
        {
            float t = timeElapsed / TimeToMove;
            float curveValue = _MovementCurve.Evaluate(t);
            
            _componentsTransforms[index].position = Vector3.Lerp(startPosition, newPosition, curveValue);

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        if (onCoroutineFinished != null)
        {
            onCoroutineFinished.Invoke();
        }
        
        ThenBursted?.Invoke(true);
    }

    private void OnDestroy()
    {
        _mainUI.OnItemButtonPressed -= ChangeBurstState;
    }
}