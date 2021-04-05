using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBound : MonoBehaviour
{
    private Camera cameraMain;
    [SerializeField] private GameObject background;

    private static ScreenBound instance;

    public static ScreenBound Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        cameraMain = Camera.main;

        if (!cameraMain.orthographic)
        {
            cameraMain.orthographic = true;
        }

        float height = cameraMain.orthographicSize * 2;
        float width = height * cameraMain.aspect;
        float depth = Mathf.Abs(cameraMain.transform.position.z - background.transform.position.z) * 2;

        SpaceSelf(width, height, depth);
    }
    
    private void SpaceSelf(float h, float w, float d)
    {
        gameObject.transform.localScale = new Vector3(h, w, d);
    }

    public float GetHeight()
    {
        return gameObject.transform.localScale.y;
    }

    public float GetWidth()
    {
        return gameObject.transform.localScale.x;
    }
}
