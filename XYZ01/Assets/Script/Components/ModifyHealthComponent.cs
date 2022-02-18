using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Components
{
    public class ModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] int _deltaHP;

        public void ApplyHealth(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ModifyHealth(_deltaHP);
            }
        }
    }
}
