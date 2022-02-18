using System.Collections;
using UnityEngine;
using PixelCrew.Creatures;

namespace PixelCrew.Components
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] Transform _dustTransform;
        [SerializeField] private float _alphaTime = 1;
        [SerializeField] private float _moveTime = 1;

        public void Teleport(GameObject target)
        {
            StartCoroutine(AnimateTeleport(target));
        }

        private IEnumerator AnimateTeleport(GameObject target)
        {
            var sprite = target.GetComponent<SpriteRenderer>();
            var input = target.GetComponent<Hero>();
            SetLockInput(input, true);
          
            yield return AlphaAnimation(sprite, 0);
            target.SetActive(false);

            yield return MoveAnimation(target);

            target.SetActive(true);
            yield return AlphaAnimation(sprite, 1);
            SetLockInput(input, false);
        }

        private IEnumerator MoveAnimation(GameObject target)
        {
            var moveTime = 0f;
            while (moveTime < _moveTime)
            {
                var progress = moveTime += Time.deltaTime;
                target.transform.position = Vector3.Lerp(target.transform.position, _dustTransform.position, progress);

                yield return null;
            }
        }

        void SetLockInput(Hero input, bool isLocked)
        {
            if (input != null)
            {
                input.enabled = !isLocked;
            }
        }


        private IEnumerator AlphaAnimation(SpriteRenderer sprite, float destAlpha)
        {
            var alphaTime = 0f;
            var spriteAlpha = sprite.color.a;
            while (alphaTime < _alphaTime)
            {
                alphaTime += Time.deltaTime;
                var progress = alphaTime / _alphaTime;
                var tmpAlpha = Mathf.Lerp(spriteAlpha, destAlpha, progress);
                var color = sprite.color;
                color.a = tmpAlpha;
                sprite.color = color;

                yield return null;
            }
        }
    }


}
