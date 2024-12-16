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
        UpdateSurviveSlider();
    }
    void UpdateSurviveSlider()
    {
        surviveSlider.value = GameManager.Instance.amount/GameManager.Instance.maxAmount;
    }
}
