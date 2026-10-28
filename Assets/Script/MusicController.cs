using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    private static MusicController instance;
    private AudioSource audioSource;

    [Header("Audio Clips")]
    public AudioClip menuMusic;
    public AudioClip gameplayMusic;

    private bool isDead = false;

    void Awake()
    {
        // حماية متقدمة لمنع النسخ المكررة من إحداث أخطاء
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();

            // ربط الحدث هنا لضمان عمله فقط للنسخة الأصلية
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        // إزالة الربط فقط إذا تم تدمير النسخة الأصلية (عند إغلاق اللعبة)
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isDead = false;

        if (scene.name == "MainMenu")
        {
            PlayMusic(menuMusic);
        }
        else if (scene.name == "SampleScene")
        {
            PlayMusic(gameplayMusic);
        }
    }

    void PlayMusic(AudioClip clip)
    {
        if (audioSource != null && clip != null && audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void StopMusicOnDeath()
    {
        isDead = true;
        // فحص إضافي للتأكد من أن الكائن لم يتم تدميره قبل إيقافه
        if (audioSource != null && audioSource.isActiveAndEnabled)
        {
            audioSource.Stop();
        }
    }
}