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
        #endregion

        #region EVENTS    
        public event Action<Star> onStarParticleSpawned;
        public event Action onStarComboChanged;
        #endregion

        public void SpawnStarPartilce(Vector3 position)
        {
            Star star = Instantiate(starPrefab, position, Quaternion.identity);
            totalStar += comboStar;
            ComboStar++;
            onStarParticleSpawned?.Invoke(star);
        }
    }
}