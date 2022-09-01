using UnityEngine;

namespace CubeMatch
{
    public class Cube : MonoBehaviour
    {
        private CubeMovement myMovementScript;

        private CubeInfo cubeInfo;
        private int myMatchAreaIndex;
        private bool isCollected;

        #region Properties
        public CubeInfo CubeInfo { get => cubeInfo; }
        public int MyMatchAreaIndex
        {
            get => myMatchAreaIndex;
            set
            {
                myMatchAreaIndex = value;
                isCollected = true;
            }
        }
        #endregion

        #region MonoBehaviour METHODS
        private void Awake()
        {
            myMovementScript = GetComponent<CubeMovement>();
        }

        private void OnEnable()
        {
            StaticEvents.onCubeHinted += OnCubeHinted;
        }

        private void OnDisable()
        {
            StaticEvents.onCubeHinted -= OnCubeHinted;
        }
        #endregion

        #region EVENT LISTENERS
        private void OnCubeHinted(CubeInfo info)
        {
            if (isCollected || cubeInfo != info) return;

            StaticEvents.onCubePicked?.Invoke(this);
        }
        #endregion

        public void Initialize(CubeInfo cubeInfo)
        {
            this.cubeInfo = cubeInfo;
            GetComponentInChildren<MeshRenderer>().material = cubeInfo.CubeMaterial;
        }

        public void MoveTo(Vector3 targetPos, bool onLocal = false, System.Action callback = null)
        {
            myMovementScript.MoveTo(targetPos, onLocal, callback);
        }
    }
}