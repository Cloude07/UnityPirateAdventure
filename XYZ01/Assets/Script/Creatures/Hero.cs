using PixelCrew.Components;
using PixelCrew.Utils;
using PixelCrew.Model;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.Creatures
{
    public class Hero : Creature
    {
        [SerializeField] private CheckCircleOverlap _interactionCheck;
        [SerializeField] private LayerCheck _wallCheck;

        [SerializeField] private float _fallDownVelocity;
        [SerializeField] private float _ineractionRadius;

        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;


         
       [Space] [Header("Particles")]

        [SerializeField] private ParticleSystem _hitParticals;

        private readonly Collider2D[] _ineractionResult = new Collider2D[1];

        [SerializeField] Text _coinText;

        bool _allowDoubleJump;
        private bool _isOnWall;

        private GameSession session;
        private float _defaltGravityScale;



        protected override void Awake()
        {
            base.Awake();
            _defaltGravityScale = Rigidbody.gravityScale;
        }



        private void Start()
        {
            session = FindObjectOfType<GameSession>();
            var health = GetComponent<HealthComponent>();

            health.SetHealth(session.Data.Hp);
            UpdateHeroWeapon();
        }
        public void OnHealthChanged(int currentHealth)
        {
            session.Data.Hp = currentHealth;
        }



        protected override void Update()
        {
            base.Update();
            if (_wallCheck.IsTouchingLayer && Direction.x == transform.localScale.x)
            {
                _isOnWall = true;
                Rigidbody.gravityScale = 0;
            }
            else
            {
                _isOnWall = false;
                Rigidbody.gravityScale = _defaltGravityScale;
            }
        }



       protected override float CalculateYVelocity()
        {
            var isJumpingPressing = Direction.y > 0;

            if (IsGrounded || _isOnWall)
            {
                _allowDoubleJump = true;
            }
            
            if(!isJumpingPressing && _isOnWall)
            {
                return 0f;
            }
        
            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVelocity)
        {
            
            if (!IsGrounded && _allowDoubleJump)
            {
                _particles.Spawn("Jump");
                _allowDoubleJump = false;
                return _jumpSpeed;

            }

            return base.CalculateJumpVelocity(yVelocity);
        }


        public void AddCoin(int coins)
        {
            session.Data.Coins += coins;
            _coinText.text = ("Coin: " + session.Data.Coins);
        }

        public override void TakeDame()
        {
            base.TakeDame();
           if (session.Data.Coins > 0)
            {
                SpawnCoins();
            }
        }

        void SpawnCoins()
        {
            var numCoinToDispose = Mathf.Min(session.Data.Coins, 5);
            session.Data.Coins -= numCoinToDispose;

            var burst = _hitParticals.emission.GetBurst(0);
            burst.count = numCoinToDispose;
            _hitParticals.emission.SetBurst(0, burst);
            _hitParticals.gameObject.SetActive(true);
            _hitParticals.Play();
            _coinText.text = "Coin: " + session.Data.Coins;
        }


        public void Ineract()
        {
            _interactionCheck.Check();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.IsInLayer(_groundLayer))
            {
                var contact = collision.contacts[0];
                //relativeVelocity - относительная скорость двух колайдеров
                if (contact.relativeVelocity.y >= _fallDownVelocity)
                {
                    _particles.Spawn("Fall");
                }
            }
        }

        public override void Attack()
        {          
            if (!session.Data.IsArmed) return;

            base.Attack();
        }



        public void ArmHero()
        {
            session.Data.IsArmed = true;
            UpdateHeroWeapon();
        }

        private void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = session.Data.IsArmed ? _armed : _disarmed;
        }
    }
}
