using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    public UIManager UIManager;

    [SerializeField]
    public Player Player;

    [SerializeField]
    public Weapons Weapons;



    private Weapons _weapons;

    private UIManager _uiManager;

    private Player _player;


    public float amount;
    public float maxAmount = 50;
    public float reduceRate = 1f;
    public float increaseAmount=10;

    public static GameManager Instance;

    public Transform[] points;
    public Transform randomPoint;

    PlayerInput input;

    private void OnEnable()
    {
        input.Enable();
        input.Levels.Esc.performed += ctx => BackToLevelMenu();
    }

    private void OnDisable()
    {
        input.Disable();
        input.Levels.Esc.performed -= ctx => BackToLevelMenu();
    }





    void Awake()
    {
        _player = Player;
        _uiManager = UIManager;
        _weapons = Weapons;
        Instance = this;
        amount = maxAmount;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        input= new PlayerInput();
    }

    private void Start()
    {
        StartCoroutine(ReduceAmount());
    }


    void Update()
    {
        
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


    public void GameOver()
    {
        if(_uiManager.surviveSlider.value<=0 || _player.isDead)
        {
            Time.timeScale = 0;
            print("You Died");
        }
    }


    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    public void BackToLevelMenu()
    {
        SceneManager.LoadScene("LevelsMenu");
    }

}
