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
    public GameObject pauseMenu, gameOverMenu,youWinMenu;
    PlayerInput input;

    private void OnEnable()
    {
        input.Enable();
        input.Levels.Esc.performed +=ctx=> PauseGame();
    }

    private void OnDisable()
    {
        input.Disable();
        input.Levels.Esc.performed -= ctx => PauseGame();
    }





    void Awake()
    {
        _player = Player;
        _uiManager = UIManager;
        _weapons = Weapons;
        Instance = this;
        amount = maxAmount;
        Time.timeScale = 1;
        
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
        if(amount<=0 || _player.isDead)
        {
            Time.timeScale = 0;
            gameOverMenu.SetActive(true);
            youWinMenu.SetActive(false);
            pauseMenu.SetActive(true);
        }
    }


    


    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    public void QuitGame()
    {
        Application.Quit();
    }


    public void PauseGame()
    {
        gameOverMenu.SetActive(false);
        youWinMenu.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeMenu()
    {
        gameOverMenu.SetActive(false);
        youWinMenu.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }


    public void NextLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

}
