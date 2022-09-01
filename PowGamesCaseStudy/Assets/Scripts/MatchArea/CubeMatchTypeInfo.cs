using System;
using System.Collections.Generic;
using UnityEngine;

namespace CubeMatch
{
    [CreateAssetMenu(menuName = "Cube Match Type Info Holder", fileName = "MatchTypeInfoHolder")]
    public class CubeMatchTypeInfo : ScriptableObject
    {
        private List<CubeInfo> cubeTypes;
        private Dictionary<CubeInfo, List<Cube>> pickedCubes;

        #region PROPERTIES
        public List<CubeInfo> CubeTypes { get => cubeTypes; }
        public Dictionary<CubeInfo, List<Cube>> PickedCubes { get => pickedCubes; }
        #endregion

        #region EVENTS
        public event Action<CubeInfo> onNewCubePicked;
        public Action<int, int> onMatchCompleted;
        #endregion

        public void Initialize()
        {
            cubeTypes = new List<CubeInfo>();
            pickedCubes = new Dictionary<CubeInfo, List<Cube>>();
        }

        public void AddNewCubeType(CubeInfo cubeInfo)
        {
            cubeTypes.Add(cubeInfo);
        }

        public void CubePicked(Cube pickedCube)
        {
            if (pickedCubes.ContainsKey(pickedCube.CubeInfo))
            {
                pickedCubes[pickedCube.CubeInfo].Add(pickedCube);
            }
            else
            {
                pickedCubes.Add(pickedCube.CubeInfo, new List<Cube>() { pickedCube });
            }

            onNewCubePicked?.Invoke(pickedCube.CubeInfo);
        }

        public void CubesMatched(CubeInfo cubeInfo)
        {
            pickedCubes.Remove(cubeInfo);
            cubeTypes.Remove(cubeInfo);
        }
    }
}