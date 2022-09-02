using System;
using System.Collections.Generic;
using UnityEngine;
using CubeMatch.StarSystem;

namespace CubeMatch.MatchArea
{
    public class MatchChecker : MonoBehaviour
    {
        [SerializeField] private StarSystemInfo starInfo;
        [SerializeField] private CubeMatchTypeInfo cubeMatchInfo;

        #region MonoBehaviour METHODS
        private void OnEnable()
        {
            cubeMatchInfo.onNewCubePicked += MatchCheck;
        }

        private void OnDisable()
        {
            cubeMatchInfo.onNewCubePicked -= MatchCheck;
        }
        #endregion

        #region EVENT LISTENERS
        private void MatchCheck(CubeInfo cubeInfo)
        {
            if (!cubeMatchInfo.IsMatchPossible(cubeInfo)) return;

            List<Cube> list = cubeMatchInfo.PickedCubes[cubeInfo];
            cubeMatchInfo.CubesMatched(cubeInfo);

            Vector3 targetPos = Vector3.zero;
            for (int i = 0; i < list.Count; i++)
            {
                targetPos += list[i].transform.position;
            }

            targetPos /= 3;

            int firstIndex = list[0].MyMatchAreaIndex;
            int lastIndex = list[2].MyMatchAreaIndex;

            for (int i = 0; i < list.Count; i++)
            {

                Cube cube = list[i];
                System.Action action;

                if (i == 0)
                {
                    action = () =>
                    {
                        cubeMatchInfo.onMatchCompleted?.Invoke(firstIndex, lastIndex);
                        starInfo.SpawnStarPartilce(targetPos);

                        Destroy(cube.gameObject);
                    };
                }
                else
                {
                    action = () =>
                    {
                        Destroy(cube.gameObject);
                    };
                }

                cube.MoveTo(targetPos, false, action);
            }
        }
        #endregion
    }
}