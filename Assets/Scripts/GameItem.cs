using System;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    public string desc = null;

    private ItemManager _itemManager;

    // Start is called before the first frame update
    void Start()
    {
        _itemManager = GameObject.Find("UIManager").GetComponent<ItemManager>();
        if (_itemManager == null)
            Debug.LogError("No se encuentra el componente ItemManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            if (transform.name.Contains("Worn_Key"))
            {
                _itemManager.UpdateItems(new Tuple<string, int, string>(_itemManager.KEY, 1, desc));
                Destroy(gameObject);
            }
            else if (transform.name.Contains("Chocolate"))
            {
                _itemManager.UpdateItems(new Tuple<string, int, string>(_itemManager.CHOCOLATE_BAR, 1, desc));
                Destroy(gameObject);
            }
            
        }
    }
}
