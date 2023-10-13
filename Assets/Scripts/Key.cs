using System;
using UnityEngine;

public class Key : MonoBehaviour
{
    private ItemManager _itemManager;

    // Start is called before the first frame update
    void Start()
    {
        _itemManager = GameObject.Find("UIManager").GetComponent<ItemManager>();
        if (_itemManager != null)
            Debug.Log("item");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            
            _itemManager.UpdateItems(new Tuple<string, int, string>(_itemManager.KEY, 1, ""));
            Destroy(gameObject); 
        }
    }
}
