using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("إعدادات التوليد")]
    public GameObject obstaclePrefab;
    public float spawnRate = 1.5f;

    [Header("معايرة الارتفاع (Y)")]
    public float groundY = -3.2f;
    public float ceilingY = 3.2f;
    public float midAirY = 0f; // الارتفاع الجديد للعقبات المعلقة في الهواء

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        float spawnX = Camera.main.transform.position.x + 15f;
        float spawnY = 0f;

        // اختيار رقم عشوائي بين 0 و 1 و 2
        int randomPos = Random.Range(0, 3);

        if (randomPos == 0)
        {
            spawnY = groundY;   // عقبة على الأرض
        }
        else if (randomPos == 1)
        {
            spawnY = ceilingY;  // عقبة على السقف
        }
        else
        {
            spawnY = midAirY;   // عقبة في الهواء
        }

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
        GameObject newObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        Destroy(newObstacle, 5f);
    }
}