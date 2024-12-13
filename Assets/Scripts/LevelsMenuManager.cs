using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelsMenuManager : MonoBehaviour
{
    public List<GameObject> levels;
    private PlayerInput input;
    private int currentLevel = 0;
    public LayerMask raycastLayerMask;


    private void Awake()
    {
        input = new PlayerInput();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Levels.Click.started += ctx => LoadSelectedScene();
    }

    private void OnDisable()
    {
        input.Disable();
        input.Levels.Click.started -= ctx => LoadSelectedScene();
    }

    void Start()
    {
          
    }

    
    void Update()
    {
        
    }


    public void ChangeLevel(int index)
    {
        if (currentLevel+index < 0 || currentLevel+index > levels.Count - 1)
            return;
        levels[currentLevel].gameObject.SetActive(false);
        currentLevel += index;
        levels[currentLevel].gameObject.SetActive(true);
    }

    public void LoadSelectedScene()
    {
        Vector3 screenPosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, raycastLayerMask))
        {
            SceneManager.LoadScene(hit.collider.gameObject.name);
        }
    }
}
