using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PixelCrew.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] int _health;
        [SerializeField] UnityEvent _onDamage;
        [SerializeField] UnityEvent _OnDie;
        [SerializeField] UnityEvent _OnHp;
        [SerializeField] HealthChangeEvent _OnChange;


        public void ModifyHealth(int healtDelta)
        {
            _health += healtDelta;
            _OnChange?.Invoke(_health);
            if(healtDelta < 0)
            {
                _onDamage?.Invoke();
            }

            if(healtDelta > 0)
            {
                _OnHp?.Invoke();
            }
            if (_health <= 0)
            {
                _OnDie?.Invoke();
            }

        }

#if UNITY_EDITOR
        /// <summary>
        /// �������� ��������� ��������
        /// </summary>
     [ContextMenu("Update Health")]
    
     void UpdateHealth()
        {
            _OnChange?.Invoke(_health);
        }
#endif

        /// <summary>
        /// ��������� ����� �� �������
        /// </summary>
        public void FallDamage()
        {
            _health--;
            dead();
        }


        void dead()
        {
            if (_health <= 0)
            {
                _OnDie?.Invoke();
            }
        }

        public void SetHealth(int health)
        {
            _health = health;
        }
    }
    [Serializable]
    public class HealthChangeEvent : UnityEvent<int>
    {

    }
}
