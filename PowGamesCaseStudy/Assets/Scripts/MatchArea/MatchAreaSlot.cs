using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CubeMatch.MatchArea
{
    public class MatchAreaSlot : MonoBehaviour
    {
        private Cube pickedCube;

        public Cube PickedCube { get => pickedCube; }

        public void AddCube(Cube cube, System.Action callback = null)
        {
            pickedCube = cube;
            pickedCube.transform.SetParent(transform, true);

            pickedCube.transform.eulerAngles = Vector3.zero;
            pickedCube.MoveTo(Vector3.zero, true, callback);
        }

        public Cube RemovePickedCube()
        {
            Cube cube = pickedCube;
            pickedCube = null;
            return cube;
        }

        public bool HaveCube()
        {
            return pickedCube != null;
        }
    }
}