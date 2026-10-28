using UnityEngine;
using UnityEngine.SceneManagement;

public class CyberRunner : MonoBehaviour
{
    [Header("إعدادات السرعة")]
    public float runSpeed = 10f;
    public float speedIncreaseRate = 0.2f;
    public float maxSpeed = 35f;

    private Rigidbody2D rb;
    private int gravityDirection = 1;
    private Camera mainCam;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
    }

    void Update()
    {
        if (runSpeed < maxSpeed)
        {
            runSpeed += speedIncreaseRate * Time.deltaTime;
        }

        rb.linearVelocity = new Vector2(runSpeed, rb.linearVelocity.y);

        if (mainCam != null)
        {
            mainCam.transform.position = new Vector3(transform.position.x, 0f, -10f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            gravityDirection *= -1;
            rb.gravityScale = 3f * gravityDirection;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
        }
    }

    // دالة الاصطدام
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // استدعاء شاشة الخسارة من مدير اللعبة بدل الإعادة الفورية
            FindObjectOfType<GameManager>().GameOver();
        }
        else if (collision.gameObject.CompareTag("Coin"))
        {
            FindObjectOfType<GameManager>().AddCoin();
            Destroy(collision.gameObject);
        }
    }
}