using UnityEngine;
using TMPro;
using DG.Tweening;

namespace CubeMatch.StarSystem
{
    public class TopStarPanel : MonoBehaviour
    {
        [SerializeField] private StarSystemInfo starInfo;
        [Space]
        [SerializeField] private TextMeshProUGUI starText;


        #region MonoBehaviour METHODS
        private void OnEnable()
        {
            starInfo.onStarParticleSpawned += OnStarCreated;
        }

        private void OnDisable()
        {
            starInfo.onStarParticleSpawned -= OnStarCreated;
        }
        #endregion

        #region EVENT LISTENERS
        private void OnStarCreated(Star star)
        {
            star.transform.SetParent(transform);
            star.transform.DOLocalMove(Vector3.zero, 1f).OnComplete(() =>
            {
                Destroy(star.gameObject);
                starText.text = starInfo.TotalStar.ToString();
            });
        }
        #endregion
    }
}