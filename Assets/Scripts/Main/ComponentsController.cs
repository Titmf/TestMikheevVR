using System;
using System.Linq;

using Cinemachine;

using UI;

using Unity.VisualScripting;

using UnityEngine;

namespace Main
{
    public class ComponentsController : MonoBehaviour
    {
        [SerializeField] private Transform _itemTransform;
        [SerializeField] private CinemachineFreeLook _cinemachineComponentFreeLookCamera;
        [SerializeField] private MainUI _mainUI;

        public delegate void PeakedComponent (String name);
        public event PeakedComponent ThenPeakedComponent;
        
        private Transform[] _componentsTransforms;
        private bool Peaked = true;
        private int? _currentIndexComponent = null;

        private void Awake()
        {
            GetComponentsTransforms();
            _mainUI.OnComponentButtonPressed += ThenPeaked;
            _mainUI.OnResetState += CleanAndReset;
        }
        
        private void ThenPeaked(int indexPeaked)
        {
            if (_currentIndexComponent == indexPeaked)
            {
                CleanAndReset();
            }
            else
            {
                SetCinemachineCamera(indexPeaked);

                _currentIndexComponent = indexPeaked;
                
                for (var i = 0; i < _componentsTransforms.Length; i++) //активация потом плавынй переход и выключение
                {
                    if (i == indexPeaked)
                    {
                        _componentsTransforms[i].GameObject().SetActive(Peaked);
                    }
                    else
                    {
                        _componentsTransforms[i].GameObject().SetActive(!Peaked);
                    }
                }
                
                ThenPeakedComponent?.Invoke("Component");
            }
        }
        private void SetCinemachineCamera(int indexPeaked)
        {
            _cinemachineComponentFreeLookCamera.LookAt = _componentsTransforms[indexPeaked];
            _cinemachineComponentFreeLookCamera.Follow = _componentsTransforms[indexPeaked];
        }

        private void CleanAndReset()
        {
            ThenPeakedComponent?.Invoke("Main Item");
            
            _currentIndexComponent = null;

            foreach (var t in _componentsTransforms)
            {
                t.GameObject().SetActive(Peaked);
            }
        }

        private void GetComponentsTransforms()
        {
            var allComponentsWithParent = _itemTransform.GetComponentsInChildren<Transform>();

            _componentsTransforms = allComponentsWithParent.
                Where(ComponentTransform => ComponentTransform != _itemTransform.transform).ToArray();
        }

        private void OnDestroy()
        {
            _mainUI.OnComponentButtonPressed -= ThenPeaked;
            _mainUI.OnResetState -= CleanAndReset;
        }
    }
}