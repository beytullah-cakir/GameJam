using UnityEngine;

public class Item : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Item toplama i�lemi
            CollectItem();
        }
    }

    void CollectItem()
    {
        // Item topland�ktan sonra yap�lacak i�lemler
        Debug.Log("Item topland�!");
        Destroy(gameObject); // Item'� yok et
    }
}