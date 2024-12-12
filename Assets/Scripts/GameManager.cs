using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public UIManager UIManager;

    [SerializeField]
    public Player Player;







    private UIManager _uiManager;

    private Player _player;


    public float amount = 50;
    public TextMeshProUGUI itemsCount;


    public static GameManager Instance;





    void Awake()
    {
        _player = Player;
        _uiManager = UIManager;
        Instance = this;
    }


    void Update()
    {
        InvokeRepeating(nameof(ReduceAmount), 1, 1);
        itemsCount.text = $"{Player.itemsCount}";
    }


    public void ReduceAmount()
    {
        amount -= 1;
        if (amount < 0f) amount = 0f;
    }
}
