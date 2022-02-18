using UnityEngine;

namespace PixelCrew
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] Transform _target;
        [SerializeField] float _damping;
        private void LateUpdate()
        {
            var destination = new Vector3(_target.position.x, _target.position.y, transform.position.z);
            //интерпол€ци€ движени€
            transform.position = Vector3.Lerp(transform.position,destination, Time.deltaTime * _damping);
        }
    }
}
