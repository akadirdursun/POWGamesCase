using UnityEngine;
using DG.Tweening;

namespace CubeMatch
{
    public class CubeMovement : MonoBehaviour
    {
        [SerializeField] private float moveTime = 0.2f;

        private Tween moveTween;

        public void MoveTo(Vector3 targetPos, bool onLocal, System.Action callback)
        {
            if (moveTween != null)
                moveTween.Kill();

            if (onLocal)
            {
                moveTween = transform.DOLocalMove(targetPos, moveTime).OnKill(() =>
                  {
                      callback?.Invoke();
                      transform.localPosition = targetPos;
                      moveTween = null;
                  });
                return;
            }

            moveTween = transform.DOMove(targetPos, moveTime).OnKill(() =>
               {
                   callback?.Invoke();
                   transform.position = targetPos;
                   moveTween = null;
               });
        }
    }
}