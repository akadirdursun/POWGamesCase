using System;
using UnityEngine;

namespace CubeMatch.StarSystem
{
    [CreateAssetMenu(menuName = "Star System Info", fileName = "NewStarSystemInfo")]
    public class StarSystemInfo : ScriptableObject
    {
        [SerializeField] private Star starPrefab;
        private int comboStar = 1;
        private int totalStar = 0;

        #region PROPERTIES
        public int ComboStar
        {
            get => comboStar;
            set
            {
                comboStar = value;
                if (comboStar < 1)
                {
                    comboStar = 1;
                    return;
                }
                onStarComboChanged?.Invoke();
            }
        }

        public int TotalStar { get => totalStar; }
        public Star StarPrefab { get => starPrefab; }
        #endregion

        #region ScriptableObject METHODS
        private void OnEnable()
        {
            totalStar = 0;
        }
        #endregion
        #region EVENTS    
        public event Action<Vector3> onStarParticleSpawned;
        public event Action onStarComboChanged;
        #endregion

        public void SpawnStarPartilce(Vector3 position)
        {
            totalStar += comboStar;
            ComboStar++;
            onStarParticleSpawned?.Invoke(position);
        }
    }
}