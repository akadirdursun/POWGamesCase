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

        private Dictionary<CubeInfo, List<Cube>> slotTypes = new Dictionary<CubeInfo, List<Cube>>();

        private MatchChecker matchChecker;

        #region PROPERTIES
        public Dictionary<CubeInfo, List<Cube>> SlotTypes { get => slotTypes; }
        #endregion

        #region EVENTS
        public event Action<CubeInfo> onNewCubeAdded;
        #endregion

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

            //slot.AddCube(cube, () => { MatchCheck(cube.CubeInfo); });
            slot.AddCube(cube, () => { onNewCubeAdded?.Invoke(cube.CubeInfo); });
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
                //cube.transform.DOLocalMove(Vector3.zero, moveTime);
                currentEmptySlot++;
                if (currentEmptySlot > lastIndex)
                {
                    break;
                }
            }
        }
        #endregion

        #region MonoBehaviour METHODS
        private void Awake()
        {
            matchChecker = GetComponent<MatchChecker>();
        }

        private void OnEnable()
        {
            StaticEvents.onCubePicked += OnCubePicked;
            matchChecker.onMatchCompleted += MoveSlotsBackwards;
        }

        private void OnDisable()
        {
            StaticEvents.onCubePicked -= OnCubePicked;
            matchChecker.onMatchCompleted -= MoveSlotsBackwards;
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
                //cubeToMove.transform.DOLocalMove(Vector3.zero, moveTime);
            }
        }
    }
}