using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CubeMatch.HintSystem
{
    public class HintSystem : MonoBehaviour
    {
        [SerializeField] private CubeMatchTypeInfo cubeMatchInfo;
        [SerializeField] private HintInfo hintInfo;        

        #region EVENT LISTENERS
        public void OnHintButtonClicked()
        {
            CubeInfo cubeInfo = null;
            if (cubeMatchInfo.PickedCubes.Count > 0)
            {
                foreach (CubeInfo key in cubeMatchInfo.PickedCubes.Keys)
                {
                    cubeInfo = key;
                    break;
                }
            }
            else if (cubeMatchInfo.CubeTypes.Count > 0)
            {
                cubeInfo = cubeMatchInfo.CubeTypes[0];
            }

            hintInfo.HintTheCubeInfo(cubeInfo);
        }
        #endregion
    }
}