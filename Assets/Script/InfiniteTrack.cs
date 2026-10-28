using UnityEngine;

public class InfiniteTrack : MonoBehaviour
{
    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (cam != null)
        {
            transform.position = new Vector3(cam.position.x, transform.position.y, transform.position.z);
        }
    }
}