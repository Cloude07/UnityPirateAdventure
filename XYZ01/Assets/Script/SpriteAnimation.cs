using System;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew
{
    //��� ���������� ������������� ����������� ��������� Sprite Renderer
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimation : MonoBehaviour
    {
        //�������� ����������
        [SerializeField]
        [Range(1, 30)] int _frameRate;
        [SerializeField]
        AnimationClip[] _clips;
        //����� ����� ��� ����������
        [SerializeField]
        UnityEvent<string> _onComplete;




        SpriteRenderer _renderer;

        float _secPerFrame; //������� ������� �������
        int _currentFrame; //������� ������ �������
        float _nextFrameTime; //����� �� ���������� ����������
        bool _isPlaying = true;

        int _currentClip;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _secPerFrame = 1f / _frameRate; //����������� ������� ����� ������ ���� ����

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
            // ��������� �� ����� ���������� ����� ���� ��� �� ������� �� �������
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
            ////������ ��������
            public Sprite[] Sprites => _sprites;
            //����������� �������� ��� ���
            public bool Loop => _loop;
            public bool AllowNextClip => _allowNextClip;
            public UnityEvent OnComplete => _onComplete;
        }

    }
}
