using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [Header("إعدادات توليد العملات")]
    public GameObject coinPrefab;
    public float spawnRate = 2f;

    [Header("حدود التوليد العشوائي (بين الأرض والسقف)")]
    // هاي الأرقام بتضمن إن العملة ما تطلع داخل الأرض أو السقف
    public float minY = -2.5f;
    public float maxY = 2.5f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnCoin();
            timer = 0f;
        }
    }

    void SpawnCoin()
    {
        float spawnX = Camera.main.transform.position.x + 15f;

        // السر هون: اختيار ارتفاع عشوائي تماماً بين الحد الأدنى والأعلى
        float spawnY = Random.Range(minY, maxY);

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
        GameObject newCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);

        Destroy(newCoin, 5f);
    }
}