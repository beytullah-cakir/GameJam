using UnityEngine;

public class Weapons : MonoBehaviour
{
    public int totalBullet;
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
        totalBullet--;
        if(totalBullet <= 0) totalBullet = 0;
        PlayerPrefs.SetInt("Bullet", totalBullet);
    }

    public void IncreaseBullet()
    {
        totalBullet+=increaseBullet;
        PlayerPrefs.SetInt("TotalBullet", totalBullet);
    }
}
