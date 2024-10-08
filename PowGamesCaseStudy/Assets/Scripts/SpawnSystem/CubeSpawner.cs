using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CubeMatch.LevelDesign.SpawnSystem
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private Cube cubePrefab;
        [SerializeField] private LevelInfo levelInfo;
        [SerializeField] private CubeMatchTypeInfo matchTypeInfo;
        [Space]
        [SerializeField] private List<CubeInfo> cubeInfos = new List<CubeInfo>();

        private void Start()
        {
            matchTypeInfo.Initialize();
            SpawnCubes();
        }

        private void SpawnCubes()
        {
            //GetPositions
            List<Vector3> spawnPositions = new List<Vector3>();
            for (int y = 0; y < levelInfo.Limits.y; y++)
            {
                for (int x = 0; x < levelInfo.Limits.x; x++)
                {
                    for (int z = 0; z < levelInfo.Limits.z; z++)
                    {
                        spawnPositions.Add(new Vector3(x, y, z));
                    }
                }
            }

            StartCoroutine(SpawnCubesRoutine(spawnPositions));
        }

        private IEnumerator SpawnCubesRoutine(List<Vector3> spawnPositions)
        {
            for (int i = 0; i < levelInfo.TotalCubeTypes; i++)
            {
                matchTypeInfo.CubeTypes.Add(cubeInfos[i]);
                for (int j = 0; j < 3; j++)
                {
                    int rs = Random.Range(0, spawnPositions.Count);
                    Cube newCube = Instantiate(cubePrefab, spawnPositions[rs] + (Vector3.up * 10f), transform.rotation, transform);
                    newCube.MoveTo(spawnPositions[rs]);
                    spawnPositions.RemoveAt(rs);

                    newCube.Initialize(cubeInfos[i]);
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
    }
}