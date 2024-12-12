using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject aimCursor, normalCursor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ManageCursor();
    }

    public void ManageCursor()
    {
        aimCursor.SetActive(GameManager.Instance.Player.isAiming);
        normalCursor.SetActive(!GameManager.Instance.Player.isAiming);
    }
}
