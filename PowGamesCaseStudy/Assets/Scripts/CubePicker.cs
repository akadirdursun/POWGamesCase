using UnityEngine;
using UnityEngine.EventSystems;

namespace CubeMatch
{
    public class CubePicker : MonoBehaviour
    {
        [SerializeField] private LayerMask cubeLayer;

        #region MonoBehaviour METHODS
        private void Update()
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButton(0))
                {
                    CubeCheck();
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    PickCube();
                }
            }
        }
        #endregion

        private void CubeCheck()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit rayHit, 20f, cubeLayer))
            {
            }
        }

        private void PickCube()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit rayHit, 20f, cubeLayer))
            {
                Cube cube = rayHit.transform.GetComponent<Cube>();
                StaticEvents.onCubePicked?.Invoke(cube);
            }
        }
    }
}