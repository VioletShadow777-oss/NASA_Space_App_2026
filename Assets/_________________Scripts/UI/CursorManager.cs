using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;
    private bool isCursorActive;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        isCursorActive = false;

        // Locks the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;

        // Hides the hardware mouse cursor
        Cursor.visible = false;
    }

    public void ToggelCursor()
    {
        if (!isCursorActive)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            isCursorActive = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            isCursorActive = false;
        }
    }
}
