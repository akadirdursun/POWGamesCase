using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CubeMatch
{
    [CreateAssetMenu(menuName = "New Cube", fileName = "NewCube")]
    public class CubeInfo : ScriptableObject
    {
        [SerializeField] private Material cubeMaterial;

        public Material CubeMaterial { get => cubeMaterial; }
    }
}