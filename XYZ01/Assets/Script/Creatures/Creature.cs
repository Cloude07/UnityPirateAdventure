using PixelCrew.Components;
using UnityEngine;

namespace PixelCrew.Creatures
{
    public class Creature : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private float _speed;
        [SerializeField] protected float _jumpSpeed;
        [SerializeField] private float _damageVelocity;
        [SerializeField] private int _damage;

        [Header("Checkers")]
        [SerializeField] protected LayerMask _groundLayer;
        [SerializeField] private LayerCheck _groundCheck;
        [SerializeField] private CheckCircleOverlap _attackRange;
        [SerializeField] protected SpawnListComponent _particles;

        protected Vector2 Direction;
        protected Rigidbody2D Rigidbody;
        protected Animator Animator;
        protected bool IsGrounded;
        private bool _isjumping;

        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int VericalVelocity = Animator.StringToHash("verical-velocity");
        private static readonly int ISRunning = Animator.StringToHash("is-running");
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int AttackKey = Animator.StringToHash("attack");

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Запись направления
        /// </summary>
        /// <param name="direction"></param>
        public void SetDirection(Vector2 direction)
        {
            Direction = direction;

        }

        protected virtual void Update()
        {
            IsGrounded = _groundCheck.IsTouchingLayer;
        }

        private void FixedUpdate()
        {
            //считаем нашу х и у кординату
            var xVelocity = Direction.x * _speed;
            var yVelocity = CalculateYVelocity();
            //устанавливаем в наш риджет боди
            Rigidbody.velocity = new Vector2(xVelocity, yVelocity);


            //передача данных для анимациии
            Animator.SetBool(IsGroundKey, IsGrounded);
            Animator.SetFloat(VericalVelocity, Rigidbody.velocity.y);
            Animator.SetBool(ISRunning, Direction.x != 0);

            UpdateSpriteDirection();

        }


        /// <summary>
        /// запись высоты и прыжка
        /// </summary>
        /// <returns></returns>
        protected virtual float CalculateYVelocity()
        {
            //записываем текущую силу по у
            var yVelocity = Rigidbody.velocity.y;
            var isJumpingPressing = Direction.y > 0;

            if (IsGrounded)
            {
                _isjumping = false;
            }

            //если прыжок 
            if (isJumpingPressing)
            {
                _isjumping = true;

                //записываем кординаты падения
                var isFalling = Rigidbody.velocity.y <= 0.001f;
                //если не падаем то возвращаем по дифолду вертикальную силу
                yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;
            }
            //иначе если сила направлена верх больше 0 то уменьш на 2
            else if (Rigidbody.velocity.y > 0 && _isjumping)
            {
                yVelocity *= 0.5f;
            }
            return yVelocity;
        }

        /// <summary>
        /// проверка прыжка и возможность двойного прыжка
        /// </summary>
        /// <param name="yVelocity"></param>
        /// <returns></returns>
        protected virtual float CalculateJumpVelocity(float yVelocity)
        {
            if (IsGrounded)
            {
                yVelocity += _jumpSpeed;
                _particles.Spawn("Jump");
            }
            return yVelocity;
        }

        /// <summary>
        /// Проверка направления 
        /// </summary>
        private void UpdateSpriteDirection()
        {
            if (Direction.x > 0)
            {
                transform.localScale = Vector3.one;
            }
            else if (Direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        /// <summary>
        /// Меттод для проверки получения урона
        /// </summary>
        public virtual void TakeDame()
        {
            _isjumping = false;
            Animator.SetTrigger(Hit);
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _damageVelocity);
        }

        /// <summary>
        /// Метод запуска анимации атаки
        /// </summary>
        public virtual void Attack()
        {      
            Animator.SetTrigger(AttackKey);
        }

        public virtual void OnAttackPlayer()
        {
            _attackRange.Check();
 
        }
    }
}
