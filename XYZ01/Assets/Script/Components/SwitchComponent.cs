using UnityEngine;

namespace PixelCrew.Components
{
    public class SwitchComponent : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        [SerializeField] bool _state;
        [SerializeField] string _animationKey;

        public void Switch()
        {
            _state = !_state;
            _animator.SetBool(_animationKey, _state);
        }

        //позволяет вызывать через инспектор
        [ContextMenu("Switch")]
        public void SwitchIt()
        {
            Switch();
        }
    }
}