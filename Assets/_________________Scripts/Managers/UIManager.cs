using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameObject playerMenues;
    private bool isPlayerMenuActive;

    [Tooltip("Used to show the active and disabled button images in systmes")]
    [SerializeField]private Sprite activeSprite;
    [SerializeField]private Sprite disableSprite;

    [Header("Mouse Click")]
    [SerializeField] private Image interactionImage;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        isPlayerMenuActive = false;
        playerMenues.SetActive(false);
    }
    public void PlayerMenuToggle()
    {
        if (!isPlayerMenuActive)
        {
            playerMenues.SetActive(true);
            isPlayerMenuActive = true;
        }
        else
        {
            playerMenues.SetActive(false);
            isPlayerMenuActive = false;
        }
    }

    public void ChangeToActiveSprite(Image buttonImage)
    {
        buttonImage.sprite = activeSprite;
    }
    public void ChangeToDisabledSprite(Image buttonImage)
    {
        buttonImage.sprite = disableSprite;
    }

    #region Interaction UI
    public void ShowInteractionUI()
    {
        interactionImage.CrossFadeAlpha(1f, 0.15f, false);
    }
    public void HideInteractionUI()
    {
        interactionImage.CrossFadeAlpha(0f, 0.15f, false);
    }
    #endregion
}
