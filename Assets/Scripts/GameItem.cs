using System;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    public string desc = null;

    private ItemManager _itemManager;
    private UIManager _uiManager;
    private HUDManager _hudManager;

    // Start is called before the first frame update
    void Start()
    {
        _itemManager = GameObject.Find("UIManager").GetComponent<ItemManager>();
        if (_itemManager == null)
            Debug.LogError("No se encuentra el componente ItemManager");
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        if (_uiManager == null)
            Debug.LogError("No se encuentra el componente UIManager");
        _hudManager = GameObject.Find("UIManager").GetComponent<HUDManager>();
        if (_hudManager == null)
            Debug.LogError("No se encuentra el componente HUDManager");
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
            }else if(transform.name.Equals("FX_LightRayRound_01"))
            {
                //_uiManager.saveButton.SetActive(true);
                _uiManager.saveButton.interactable = true;
                _uiManager.index = int.Parse(desc);
                _hudManager.UpdateInfoHUD("<b><color=#9900FF>Save Point</color></b>\nAccess the menu and select <b>Save</b> to saved your game");
                
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (transform.name.Equals("FX_LightRayRound_01"))
            {
                //_uiManager.saveButton.SetActive(false);
                _uiManager.saveButton.interactable = false;
                _hudManager.DeactivateInfoHUD();
            }
        }
    }
}
