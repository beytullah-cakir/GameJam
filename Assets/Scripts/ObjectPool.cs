using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<GameObject> bullets = new List<GameObject>();
    public GameObject bullet;
    public int bulletAmount = 30;

    public static ObjectPool instance;

    private void Awake()
    {
        instance = this;
    }



    void Start()
    {
        for(int i=0;i<bulletAmount; i++)
        {
            GameObject _bullet=Instantiate(bullet,transform);
            _bullet.SetActive(false);
            bullets.Add(_bullet);
        }
    }

    
    public GameObject GetBullet()
    {
        for (int i=0;i<bullets.Count; i++)
        {
            if(!bullets[i].activeInHierarchy)
                return bullets[i];
        }


        return null;
    }
}
