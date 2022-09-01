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
        private void OnStarCreated(Vector3 position)
        {
            Star star = Instantiate(starInfo.StarPrefab, position, Quaternion.identity, transform);
            star.MovementSequence(() =>
            {
                starText.text = starInfo.TotalStar.ToString();
            });
        }
        #endregion
    }
}