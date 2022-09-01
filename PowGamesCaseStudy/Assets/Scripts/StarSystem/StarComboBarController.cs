using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace CubeMatch.StarSystem
{
    public class StarComboBarController : MonoBehaviour
    {
        [SerializeField] private Slider comboSlider;
        [SerializeField] private TextMeshProUGUI comboText;
        [SerializeField] private StarSystemInfo starInfo;

        private Tween starComboSliderTween;

        #region MonoBehaviour METHODS
        private void OnEnable()
        {
            starInfo.onStarComboChanged += OnStarComboChanged;
        }

        private void Start()
        {
            starInfo.ComboStar = 1;
        }

        private void OnDisable()
        {
            starInfo.onStarComboChanged -= OnStarComboChanged;
        }
        #endregion

        #region EVENT LISTENERS
        private void OnStarComboChanged()
        {
            if (starComboSliderTween != null) starComboSliderTween.Kill();

            comboText.text = "x" + starInfo.ComboStar.ToString();

            comboSlider.value = 1;
            starComboSliderTween = comboSlider.DOValue(0, 2f).OnComplete(() =>
            {
                starInfo.ComboStar--;
            });
        }
        #endregion
    }
}