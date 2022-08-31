using System;
using System.Collections.Generic;
using UnityEngine;

namespace CubeMatch.MatchArea
{
    public class MatchChecker : MonoBehaviour
    {
        private CubeMatchArea matchArea;

        #region MonoBehaviour METHODS
        private void Awake()
        {
            matchArea = GetComponent<CubeMatchArea>();
        }

        private void OnEnable()
        {
            matchArea.onNewCubeAdded += MatchCheck;
        }

        private void OnDisable()
        {
            matchArea.onNewCubeAdded -= MatchCheck;
        }
        #endregion

        #region EVENTS
        public event Action<int, int> onMatchCompleted;
        #endregion

        #region EVENT LISTENERS

        private void MatchCheck(CubeInfo cubeInfo)
        {
            if (matchArea.SlotTypes[cubeInfo].Count != 3) return;

            bool isMatchCompleted = false;

            List<Cube> list = matchArea.SlotTypes[cubeInfo];
            matchArea.SlotTypes.Remove(cubeInfo);

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
                cube.MoveTo(targetPos, false,
                    () =>
                    {
                        Destroy(cube.gameObject);

                        if (!isMatchCompleted)
                        {
                            onMatchCompleted?.Invoke(firstIndex, lastIndex);
                            isMatchCompleted = true;
                        }
                    });
            }
        }

        #endregion
    }
}