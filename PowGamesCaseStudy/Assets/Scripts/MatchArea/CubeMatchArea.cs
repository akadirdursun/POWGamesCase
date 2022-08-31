using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace CubeMatch.MatchArea
{
    public class CubeMatchArea : MonoBehaviour
    {
        [SerializeField] private List<MatchAreaSlot> slots = new List<MatchAreaSlot>();

        [SerializeField] private float moveTime = 0.1f;
        private Dictionary<CubeInfo, List<Cube>> slotTypes = new Dictionary<CubeInfo, List<Cube>>();

        #region EVENT LISTENERS
        private void OnCubePicked(Cube cube)
        {
            MatchAreaSlot slot;
            if (slotTypes.ContainsKey(cube.CubeInfo))
            {
                slot = GetTargetSlot(cube);
                slotTypes[cube.CubeInfo].Add(cube);
            }
            else
            {
                slot = GetEmptySlot(cube);
                slotTypes.Add(cube.CubeInfo, new List<Cube>() { cube });
            }

            if (slot == null)
            {
                //TODO: GameOver
                return;
            }

            slot.AddCube(cube);

            cube.transform.DOLocalMove(Vector3.zero, moveTime).OnComplete(() =>
            {
                MatchCheck(cube.CubeInfo);
            });

        }
        #endregion

        #region MonoBehaviour METHODS
        private void OnEnable()
        {
            StaticEvents.onCubePicked += OnCubePicked;
        }

        private void OnDisable()
        {
            StaticEvents.onCubePicked -= OnCubePicked;
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
            int targetSlotIndex = slotTypes[cube.CubeInfo].LastItem().MyMatchAreaIndex + 1;            
            MoveSlotsOnwards(targetSlotIndex);
            slots[targetSlotIndex].AddCube(cube);
            cube.transform.DOLocalMove(Vector3.zero, moveTime);

            cube.MyMatchAreaIndex = targetSlotIndex;

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
                cubeToMove.transform.DOLocalMove(Vector3.zero, moveTime);
            }
        }

        private void MoveSlotsBackwards(int firstIndex, int lastIndex)
        {
            int currentEmptySlot = firstIndex;
            for (int i = lastIndex + 1; i < slots.Count; i++)
            {
                if (!slots[i].HaveCube()) break;

                Cube cube = slots[i].RemovePickedCube();

                slots[currentEmptySlot].AddCube(cube);
                cube.MyMatchAreaIndex = currentEmptySlot;
                cube.transform.DOLocalMove(Vector3.zero, moveTime);
                currentEmptySlot++;
                if (currentEmptySlot > lastIndex)
                {
                    break;
                }
            }
        }

        private void MatchCheck(CubeInfo cubeInfo)
        {
            if (slotTypes[cubeInfo].Count != 3) return;

            List<Cube> list = slotTypes[cubeInfo];
            slotTypes.Remove(cubeInfo);

            Vector3 targetPos = Vector3.zero;
            for (int i = 0; i < list.Count; i++)
            {
                targetPos += list[i].transform.position;
            }

            targetPos /= 3;

            list[0].transform.DOMove(targetPos, moveTime);
            list[2].transform.DOMove(targetPos, moveTime).OnComplete(() =>
            {
                MoveSlotsBackwards(list[0].MyMatchAreaIndex, list[2].MyMatchAreaIndex);
                for (int i = 0; i < list.Count; i++)
                {
                    Destroy(list[i].gameObject);
                }

            });
        }
    }
}