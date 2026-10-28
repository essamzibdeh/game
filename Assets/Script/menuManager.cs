using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // دالة بدء اللعبة (زر Start)
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); // تأكد من اسم مشهد اللعب عندك
    }

    // دالة الإعدادات (يمكنك فتح لوحة إعدادات لاحقاً)
    public void OpenSettings()
    {
        Debug.Log("فتح الإعدادات...");
        // هنا يمكنك تفعيل Panel الإعدادات لاحقاً
    }

    // دالة اختيار المراحل
    public void OpenLevelSelect()
    {
        Debug.Log("فتح قائمة المراحل...");
    }

    // دالة السكنات والشخصيات
    public void OpenSkins()
    {
        Debug.Log("فتح قائمة الشخصيات...");
    }

    // دالة لوحة الصدارة (Leaderboards)
    public void OpenLeaderboards()
    {
        Debug.Log("فتح لوحة الصدارة...");
    }

    // دالة الخروج من اللعبة نهائياً (زر Exit)
    public void QuitGame()
    {
        Debug.Log("تم إغلاق اللعبة");
        Application.Quit();
    }
}