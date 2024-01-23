using System;

using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainUI : MonoBehaviour
    {
        [SerializeField] private GameObject _scrollRect;
        [SerializeField] private Button _itemButton;
        [SerializeField] private BurstModel _burstModel;
        
        public delegate void ComponentButton (int index);
        public event ComponentButton OnComponentButtonPressed;
        
        public delegate void ItemButton ();
        public event ItemButton OnItemButtonPressed;
        
        public delegate void ResetState ();
        public event ResetState OnResetState;
        
        private bool _opened = false;

        private void Awake()
        {
            _burstModel.ThenBursted += SetInteractable;
        }
        public void ComponentButtonPress(int index)
        {
            OnComponentButtonPressed?.Invoke(index);
        }

        public void ItemButtonPress()
        {
            OnItemButtonPressed?.Invoke();
            if (_opened == false)
            {
                _opened = true;
                _scrollRect.SetActive(_opened);
                SetInteractable(false);
            }
            else
            {
                _opened = false;
                _scrollRect.SetActive(_opened);
                SetInteractable(false);
                OnResetState?.Invoke();
            }
        }

        private void SetInteractable(bool state)
        {
            _itemButton.interactable = state;
        }

        private void OnDestroy()
        {
            _burstModel.ThenBursted -= SetInteractable;
        }
    }
}