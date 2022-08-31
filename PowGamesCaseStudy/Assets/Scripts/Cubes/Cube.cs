using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CubeMatch
{
    public class Cube : MonoBehaviour
    {
        private CubeInfo cubeInfo;

        public void Initialize(CubeInfo cubeInfo)
        {
            this.cubeInfo = cubeInfo;
            GetComponentInChildren<MeshRenderer>().material = cubeInfo.CubeMaterial;
        }
    }
}