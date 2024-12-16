using UnityEngine;

public class Item : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Item toplama iþlemi
            CollectItem();
        }
    }

    void CollectItem()
    {
        // Item toplandýktan sonra yapýlacak iþlemler
        Debug.Log("Item toplandý!");
        Destroy(gameObject); // Item'ý yok et
    }
}