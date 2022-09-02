using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace CubeMatch.MatchArea
{
    public class CubeMatchArea : MonoBehaviour
    {
        [SerializeField] private CubeMatchTypeInfo cubeMatchInfo;
        [Space]
        [SerializeField] private List<MatchAreaSlot> slots = new List<MatchAreaSlot>();

        #region EVENT LISTENERS
        private void OnCubePicked(Cube cube)
        {
            if (cube.IsCollected) return;

            MatchAreaSlot slot;
            if (cubeMatchInfo.PickedCubes.ContainsKey(cube.CubeInfo))
            {
                slot = GetTargetSlot(cube);
            }
            else
            {
                slot = GetEmptySlot(cube);
            }


            if (slot == null)
            {
                //TODO: GameOver
                return;
            }

            slot.AddCube(cube, () => { cubeMatchInfo.CubePicked(cube); });
        }

        private void MoveSlotsBackwards(int firstIndex, int lastIndex)
        {
            Debug.Log("deneme");
            int currentEmptySlot = firstIndex;
            for (int i = lastIndex + 1; i < slots.Count; i++)
            {
                if (!slots[i].HaveCube()) break;

                Cube cube = slots[i].RemovePickedCube();

                slots[currentEmptySlot].AddCube(cube);
                cube.MyMatchAreaIndex = currentEmptySlot;
                currentEmptySlot++;
            }
        }
        #endregion

        #region MonoBehaviour METHODS
        private void OnEnable()
        {
            StaticEvents.onCubePicked += OnCubePicked;
            cubeMatchInfo.onMatchCompleted += MoveSlotsBackwards;
        }

        private void OnDisable()
        {
            StaticEvents.onCubePicked -= OnCubePicked;
            cubeMatchInfo.onMatchCompleted -= MoveSlotsBackwards;
        }
        #endregion

        private MatchAreaSlot GetEmptySlot(Cube cube)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (!slots[i].HaveCube())
                {
                    cube.MyMatchAreaIndex = i;
                    return slots[i];
                }
            }

            return null;
        }

        private MatchAreaSlot GetTargetSlot(Cube cube)
        {
            if (slots.LastItem().PickedCube != null) return null;

            int targetSlotIndex = cubeMatchInfo.PickedCubes[cube.CubeInfo].LastItem().MyMatchAreaIndex + 1;
            MoveSlotsOnwards(targetSlotIndex);

            cube.MyMatchAreaIndex = targetSlotIndex;

            if (targetSlotIndex >= slots.Count)
                return null;

            return slots[targetSlotIndex];
        }

        private void MoveSlotsOnwards(int firstIndexToMove)
        {
            for (int i = slots.Count - 1; i >= firstIndexToMove; i--)
            {
                if (!slots[i].HaveCube()) continue;

                Cube cubeToMove = slots[i].RemovePickedCube();

                slots[i + 1].AddCube(cubeToMove);
                cubeToMove.MyMatchAreaIndex = i + 1;
            }
        }
    }
}