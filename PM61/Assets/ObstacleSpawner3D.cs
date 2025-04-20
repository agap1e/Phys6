using UnityEngine;

public class ObstacleSpawner3D : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public int minObstacles = 3;
    public int maxObstacles = 6;

    // Границы области спавна по X и Z (Y фиксирован)
    public Vector2 areaX = new Vector2(-4.5f, 4.5f);
    public Vector2 areaZ = new Vector2(-2.5f, 2.5f);

    void Start()
    {
        int count = Random.Range(minObstacles, maxObstacles + 1);

        for (int i = 0; i < count; i++)
        {
            Vector3 pos;
            bool validPos;
            int attempts = 0;

            do
            {
                float x = Random.Range(areaX.x, areaX.y);
                float z = Random.Range(areaZ.x, areaZ.y);
                pos = new Vector3(x, obstaclePrefab.transform.position.y, z);

                Collider[] colliders = Physics.OverlapSphere(pos, 0.75f);
                validPos = colliders.Length == 0;

                attempts++;
                if (attempts > 20) break;
            } while (!validPos);

            Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            Instantiate(obstaclePrefab, pos, randomRotation);
        }
    }
}