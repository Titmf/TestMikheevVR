using UnityEngine;

namespace UI
{
    public class ShowButton :MonoBehaviour
    {
        [SerializeField] private GameObject _scrollRect;
        
        private bool _opened = false;

        public void OnButtonPress()
        {
            if (_opened == false)
            {
                _opened = true;
                _scrollRect.SetActive(true);
            }

            else
            {
                _opened = false;
                _scrollRect.SetActive(false);
            }
        }
    }
}