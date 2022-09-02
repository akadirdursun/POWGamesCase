using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CubeMatch.HintSystem
{
    public class HintUI : MonoBehaviour
    {
        [SerializeField] private HintInfo hintInfo;
        [Space]
        [SerializeField] private TextMeshProUGUI hintCountText;
        [SerializeField] private Button hintButton;

        #region MonoBehaviour METHODS
        private void OnEnable()
        {
            hintInfo.onCubeInfoHinted += OnCubeInfoHinted;
        }

        private void Start()
        {
            OnCubeInfoHinted(null);
        }

        private void OnDisable()
        {
            hintInfo.onCubeInfoHinted -= OnCubeInfoHinted;
        }
        #endregion

        #region EVENT LISTENERS
        private void OnCubeInfoHinted(CubeInfo info)
        {
            hintCountText.text = hintInfo.HintCount.ToString();

            if (hintInfo.HintCount == 0)
            {
                hintButton.interactable = false;
            }
        }
        #endregion
    }
}