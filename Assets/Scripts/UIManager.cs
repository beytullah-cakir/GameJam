using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject aimCursor, normalCursor;
    public Slider surviveSlider;
    
    void Start()
    {
        surviveSlider.value = GameManager.Instance.maxAmount;
    }

    
    void Update()
    {
        ManageCursor();
        UpdateSurviveSlider();
    }

    public void ManageCursor()
    {
        aimCursor.SetActive(GameManager.Instance.Player.isAiming);
        normalCursor.SetActive(!GameManager.Instance.Player.isAiming);
    }


    void UpdateSurviveSlider()
    {
        surviveSlider.value = GameManager.Instance.amount/GameManager.Instance.maxAmount;
    }
}
