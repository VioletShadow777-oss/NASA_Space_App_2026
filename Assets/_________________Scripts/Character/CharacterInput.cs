using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    public void TabActios(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            UIManager.Instance.PlayerMenuToggle();
        }
    }
}
