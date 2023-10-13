using System;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string desc = null;

    private ItemManager _itemManager;

    // Start is called before the first frame update
    void Start()
    {
        _itemManager = GameObject.Find("UIManager").GetComponent<ItemManager>();
        if (_itemManager != null)
            Debug.Log("item");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            _itemManager.UpdateItems(new Tuple<string, int, string>(_itemManager.KEY, 1, desc));
            Destroy(gameObject); 
        }
    }
}
