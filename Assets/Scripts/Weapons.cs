using UnityEngine;

public class Weapons : MonoBehaviour
{
    public float totalBullet;
    public float currentBullet;
    public float increaseBullet;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ReduceBUllet()
    {
        currentBullet--;
    }

    public void IncreaseBullet()
    {
        totalBullet+=increaseBullet;
    }
}
