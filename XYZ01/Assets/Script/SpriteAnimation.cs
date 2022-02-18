using System;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew
{
    //при добавлении автоматически добовляется компонент Sprite Renderer
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimation : MonoBehaviour
    {
        //скорость обновления
        [SerializeField]
        [Range(1, 30)] int _frameRate;
        [SerializeField]
        AnimationClip[] _clips;
        //эвент когда все закончится
        [SerializeField]
        UnityEvent<string> _onComplete;




        SpriteRenderer _renderer;

        float _secPerFrame; //сколько выходит времени
        int _currentFrame; //текущий индекс спрайта
        float _nextFrameTime; //время до следующего обновление
        bool _isPlaying = true;

        int _currentClip;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _secPerFrame = 1f / _frameRate; //расчитываем сколько будет длится один кадр

            StartAnimation();
        }

        private void OnBecameVisible()
        {
            enabled = _isPlaying;
        }

        private void OnBecameInvisible()
        {
            enabled = false;
        }

        public void SetClip(string clipName)
        {
            for (var i = 0; i < _clips.Length; i++)
            {
                if (_clips[i].Name == clipName)
                {
                    _currentClip = i;
                    StartAnimation();
                    return;
                }
            }
            enabled = _isPlaying = false;
        }

        void StartAnimation()
        {
            _nextFrameTime = Time.time + _secPerFrame;
            enabled = _isPlaying = true;
            _currentFrame = 0;
        }

        private void OnEnable()
        {
            _nextFrameTime = Time.time + _secPerFrame;
        }

        void Update()
        {
            // наступила ли время следующего кадра если нет то выходим из функции
            if (_nextFrameTime > Time.time) return;

            var clip = _clips[_currentClip];
            if (_currentFrame >= clip.Sprites.Length)
            {
                if (clip.Loop)
                {
                    _currentFrame = 0;
                }
                else
                {
                    enabled = _isPlaying = clip.AllowNextClip;
                    clip.OnComplete?.Invoke();
                    _onComplete?.Invoke(clip.Name);
                    if (clip.AllowNextClip)
                    {
                        _currentFrame = 0;
                        _currentClip = (int)Mathf.Repeat(_currentClip + 1, _clips.Length);
                    }

                }
                return;
            }

            _renderer.sprite = clip.Sprites[_currentFrame];

            _nextFrameTime += _secPerFrame;
            _currentFrame++;
        }

        [Serializable]
        public class AnimationClip
        {
            [SerializeField] string _name;
            [SerializeField] Sprite[] _sprites;
            [SerializeField] bool _loop;
            [SerializeField] bool _allowNextClip;
            [SerializeField] UnityEvent _onComplete;

            public string Name => _name;
            ////список обьектов
            public Sprite[] Sprites => _sprites;
            //зацикливать анимацию или нет
            public bool Loop => _loop;
            public bool AllowNextClip => _allowNextClip;
            public UnityEvent OnComplete => _onComplete;
        }

    }
}
