using UnityEngine;

namespace CubeMatch
{
    public class Cube : MonoBehaviour
    {
        private CubeMovement myMovementScript;

        private CubeInfo cubeInfo;
        private int myMatchAreaIndex;

        #region Properties
        public CubeInfo CubeInfo { get => cubeInfo; }
        public int MyMatchAreaIndex { get => myMatchAreaIndex; set => myMatchAreaIndex = value; }
        #endregion

        #region MonoBehaviour METHODS
        private void Awake()
        {
            myMovementScript = GetComponent<CubeMovement>();
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