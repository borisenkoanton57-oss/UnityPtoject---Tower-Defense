using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomCursor : MonoBehaviour
{
    public Texture2D normalCursor;
    public Texture2D clickCursor;
    public Texture2D sellCursor;
    public Texture2D buildCursor;
    private static CustomCursor instance;
    private bool sellMode = false;
    private bool buildMode = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        Cursor.SetCursor(normalCursor,Vector2.zero,CursorMode.Auto);
    }

    void Update()
    {
       if (sellMode)
        {
            Cursor.SetCursor(sellCursor, Vector2.zero, CursorMode.Auto);
            return;
        }

        if (buildMode)
        {
            Cursor.SetCursor(buildCursor, Vector2.zero, CursorMode.Auto);
            return;
        }

        if (Input.GetMouseButton(0))
        {
            Cursor.SetCursor(clickCursor, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
        }
    }

    public void SetSellCursor(bool active)
    {
        sellMode = active;

        if (sellMode)
        {
            Cursor.SetCursor(sellCursor, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
        }
    } 

    public void SetBuildCursor(bool active)
    {
        buildMode = active;

        if (buildMode)
        {
        Cursor.SetCursor(buildCursor, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
        }
    }  
}
