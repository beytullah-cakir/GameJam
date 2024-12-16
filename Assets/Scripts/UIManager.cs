using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider surviveSlider;
    public TextMeshProUGUI bulletCount;
    
    void Start()
    {
        surviveSlider.value = GameManager.Instance.maxAmount;
    }

    
    void Update()
    {
        UpdateSurviveSlider();
        bulletCount.text = GameManager.Instance.Weapons.totalBullet.ToString();
    }
    void UpdateSurviveSlider()
    {
        surviveSlider.value = GameManager.Instance.amount/GameManager.Instance.maxAmount;
    }



}
