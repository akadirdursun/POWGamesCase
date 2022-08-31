using UnityEngine;

namespace CubeMatch
{
    public class Cube : MonoBehaviour
    {
        private CubeInfo cubeInfo;
        private int myMatchAreaIndex;

        #region Properties
        public CubeInfo CubeInfo { get => cubeInfo; }
        public int MyMatchAreaIndex { get => myMatchAreaIndex; set => myMatchAreaIndex = value; }
        #endregion

        public void Initialize(CubeInfo cubeInfo)
        {
            this.cubeInfo = cubeInfo;
            GetComponentInChildren<MeshRenderer>().material = cubeInfo.CubeMaterial;
        }
    }
}