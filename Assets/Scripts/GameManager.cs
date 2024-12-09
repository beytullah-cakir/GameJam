using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public UIManager UIManager;

    [SerializeField]
    public Player Player;

    





    private UIManager _uiManager;

    private Player _player;

    
    void Awake()
    {
        _player = Player;
        _uiManager = UIManager;
    }

    
    void Update()
    {
        
    }
}
