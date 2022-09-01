using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CubeMatch
{
    public class CubeStackRotater : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed;

        #region MonoBehaviour METHODS
        private void Update()
        {
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Rotate();
            }
        }
        #endregion

        private void Rotate()
        {
            float xRotation = -Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            float yRotation = -Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;

            transform.RotateAround(transform.position, Vector3.up, xRotation);
            transform.RotateAround(transform.position, Vector3.left, yRotation);
        }
    }
}