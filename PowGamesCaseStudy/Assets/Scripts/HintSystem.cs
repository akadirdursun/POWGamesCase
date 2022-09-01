using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CubeMatch.HintSystem
{
    public class HintSystem : MonoBehaviour
    {
        [SerializeField] private CubeMatchTypeInfo cubeMatchInfo;

        #region EVENT LISTENERS
        public void OnHintButtonClicked()
        {
            if (cubeMatchInfo.PickedCubes.Count > 0)
            {
                foreach (CubeInfo key in cubeMatchInfo.PickedCubes.Keys)
                {
                    StaticEvents.onCubeHinted?.Invoke(key);
                    break;
                }
            }
            else if (cubeMatchInfo.CubeTypes.Count > 0)
            {
                StaticEvents.onCubeHinted?.Invoke(cubeMatchInfo.CubeTypes[0]);
            }
        }
        #endregion
    }
}