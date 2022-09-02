using System;
using UnityEngine;

namespace CubeMatch.HintSystem
{
    [CreateAssetMenu(menuName = "Hint Info", fileName = "HintInfo")]
    public class HintInfo : ScriptableObject
    {
        [SerializeField] private int hintCount = 2;

        public int HintCount { get => hintCount; }

        #region EVENTS
        public event Action<CubeInfo> onCubeInfoHinted;
        #endregion

        public void HintTheCubeInfo(CubeInfo cubeInfo)
        {
            if (hintCount == 0 || cubeInfo == null) return;

            hintCount--;
            onCubeInfoHinted?.Invoke(cubeInfo);
        }
    }
}