using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class CheckCircleOverlap : MonoBehaviour
{
    [SerializeField] float _radius = 1f;
    [SerializeField] LayerMask _mask;
    [SerializeField] private string[] _tags;
    [SerializeField] private OnOverlapEvant _onOverlap;
    readonly Collider2D[] _interactionResult = new Collider2D[10];
  

    private void OnDrawGizmosSelected()
    {
        Handles.color = HandlesUtils.TransparentRed;
        Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
    }

    public void Check()
    {
        var size = Physics2D.OverlapCircleNonAlloc(
          transform.position,
          _radius,
          _interactionResult,
          _mask);

        for (var i = 0; i < size; i++)
        {
            var overlapResult = _interactionResult[i];
            //перебор тегов, если один тег верный то передаст true
            var isInTags =  _tags.Any(tag => overlapResult.CompareTag(tag));
            if (isInTags)
            {
                _onOverlap?.Invoke(_interactionResult[i].gameObject);
            }
        }
    }
    [Serializable]
    public class OnOverlapEvant: UnityEvent<GameObject>
    {

    }


}
