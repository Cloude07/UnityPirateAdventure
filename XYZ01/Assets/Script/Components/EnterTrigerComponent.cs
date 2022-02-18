using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class EnterTrigerComponent : MonoBehaviour
    {
        [SerializeField] string _tag;
        [SerializeField] EnterEvent _action;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {

            //если есть пересечение обьекта с тегом
            if (collision.gameObject.CompareTag(_tag))
            {
             
                    _action?.Invoke(collision.gameObject);
                
            }
        }

    }
}
