using UnityEngine;

public class PlayerSkinSetup : MonoBehaviour
{
    [Header("صور السكنات (رتبها من 0 لـ 9 نفس ترتيب الأزرار)")]
    public Sprite[] skinSprites;

    void Start()
    {
        // جلب المكون المسؤول عن رسم شكل اللاعب
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        // قراءة السكن اللي اختاره اللاعب من القائمة (إذا ما اختار رح يعطيه 0 كافتراضي)
        int selectedSkinIndex = PlayerPrefs.GetInt("SelectedSkin", 0);

        // التأكد إن المكون موجود وإن رقم السكن موجود جوا القائمة عشان ما يصير خطأ
        if (sr != null && selectedSkinIndex >= 0 && selectedSkinIndex < skinSprites.Length)
        {
            // تغيير شكل اللاعب للصورة المطلوبة!
            sr.sprite = skinSprites[selectedSkinIndex];
        }
    }
}