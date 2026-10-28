using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam;

    [Header("سرعة حركة الطبقة (0 يعني ثابتة، 1 يعني بتمشي مع الكاميرا)")]
    public float parallaxEffect;

    void Start()
    {
        startpos = transform.position.x;
        // حساب عرض الصورة عشان اللعبة تعرف متى تكررها
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        // تحريك الصورة بناءً على حركة الكاميرا
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        // تكرار الصورة للأمام أو للخلف لما الكاميرا تتجاوزها
        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}