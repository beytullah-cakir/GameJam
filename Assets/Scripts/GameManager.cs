using System.Collections;
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


    public float amount;
    public float maxAmount = 50;
    public float reduceRate = 1f;
    public float increaseAmount=10;
    public TextMeshProUGUI itemsCount;


    public static GameManager Instance;

    public Transform[] points;
    public Transform randomPoint;





    void Awake()
    {
        _player = Player;
        _uiManager = UIManager;
        Instance = this;
        amount = maxAmount;
    }

    private void Start()
    {
        StartCoroutine(ReduceAmount());
    }


    void Update()
    {
        itemsCount.text = $"{Player.itemsCount}";
    }

    public Transform RandomPoints()
    {
        int random=Random.Range(0, points.Length);
        return points[random];
    }


    IEnumerator ReduceAmount()
    {
        while (true)
        {
            amount--;
            if (amount < 0f) amount = 0f;
            yield return new WaitForSeconds(reduceRate);
        }
    }


    public void IncreaseAmount()
    {
        amount += increaseAmount;
        if(amount>maxAmount) amount = maxAmount;
    }

}
