using UnityEngine;

namespace UI
{
    public class MainUI : MonoBehaviour
    {
        [SerializeField] private GameObject _scrollRect;
        
        public delegate void ComponentButton (int index);
        public event ComponentButton OnComponentButtonPressed;
        
        public delegate void ItemButton ();
        public event ItemButton OnItemButtonPressed;
        
        public delegate void ResetState ();
        public event ResetState OnResetState;
        
        private bool _opened = false;
        
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
                _scrollRect.SetActive(true);
            }
            else
            {
                _opened = false;
                _scrollRect.SetActive(false);
                OnResetState?.Invoke();
            }
        }
    }
}