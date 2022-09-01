using UnityEngine;
using DG.Tweening;

namespace CubeMatch.StarSystem
{
    public class Star : MonoBehaviour
    {
        public void MovementSequence(System.Action callback)
        {
            Sequence sequence = DOTween.Sequence();
            Tween tween = transform.DOPunchScale(Vector3.one, 0.2f);
            sequence.Append(tween);

            tween = transform.DOLocalMove(Vector3.zero, 0.5f).OnComplete(() =>
            {
                Destroy(gameObject);
                callback?.Invoke();
            });
            sequence.Append(tween);
        }
    }
}