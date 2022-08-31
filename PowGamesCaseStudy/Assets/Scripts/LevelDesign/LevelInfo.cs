using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CubeMatch.LevelDesign
{
    [CreateAssetMenu(menuName = "New LevelInfo", fileName = "NewLevelInfo")]
    public class LevelInfo : ScriptableObject
    {
        [SerializeField] private Vector3Int limits;
        [SerializeField][Range(1, 14)] private int totalCubeTypes = 2;

        private void OnValidate()
        {
            if (limits.x * limits.y * limits.z < totalCubeTypes * 3)
            {
                Debug.LogError(name + " does not have enough limit space for " + (totalCubeTypes * 3) + " Cubes");
            }
        }

        public Vector3Int Limits { get => limits; }
        public int TotalCubeTypes { get => totalCubeTypes; }
    }
}