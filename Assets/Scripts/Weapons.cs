using UnityEngine;

public class Weapons : MonoBehaviour
{
    public int totalBullet;
    public int currentBullet;
    public int increaseBullet;
    void Start()
    {
        PlayerPrefs.GetInt("TotalBullet");
        PlayerPrefs.GetInt("Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ReduceBUllet()
    {
        currentBullet--;
        PlayerPrefs.SetInt("Bullet", currentBullet);
    }

    public void IncreaseBullet()
    {
        totalBullet+=increaseBullet;
        PlayerPrefs.SetInt("TotalBullet", totalBullet);
    }
}
