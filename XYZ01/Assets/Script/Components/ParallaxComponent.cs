using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackGround
{
    public class ParallaxComponent : MonoBehaviour
    {
        float _length, _startpos;
        [SerializeField] GameObject _camera;
        [SerializeField] float _parallaxEffect;

        private void Start()
        {
            _startpos = transform.position.x;
            _length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        private void FixedUpdate()
        {
            float _temp = (_camera.transform.position.x * (1 - _parallaxEffect));
            float _dist = (_camera.transform.position.x * _parallaxEffect);

            transform.position = new Vector3(_startpos + _dist, transform.position.y, transform.position.z);

            if (_temp > _startpos + _length)
            {
                _startpos += _length;
            }
            else
                if (_temp < _startpos - _length)
            {
                _startpos -= _length;
            }
        }
    }
}
