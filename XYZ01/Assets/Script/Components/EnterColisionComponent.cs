using UnityEngine.Events;
using UnityEngine;
using System;


namespace PixelCrew.Components
{
    public class EnterColisionComponent : MonoBehaviour
    {
        [SerializeField] string[] _tag;
        [SerializeField] EnterEvent _action;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            for (int i = 0; i < _tag.Length; i++)
            {
                if (collision.gameObject.CompareTag(_tag[i]))
                {
                    _action?.Invoke(collision.gameObject);
                }
            }
        }


    }
    [Serializable]
    public class EnterEvent : UnityEvent<GameObject>
    {

    }
}
