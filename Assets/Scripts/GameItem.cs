using System;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    public const string INFO_SAVE = "<b><color=#9900FF>Save Point</color></b>\nGet in and access the menu\nSelect <b>Save</b> to saved your game";

    public string desc = null;

    [SerializeField]
    private AudioClip m_ItemSound;

    private ItemManager _itemManager;
    private UIManager _uiManager;
    private HUDManager _hudManager;
    private Player _player;
    private AudioSource _audioSource;

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
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();

        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            Debug.Log(other.name);
            if (transform.name.Contains("Worn_Key"))
            {
                _audioSource.PlayOneShot(m_ItemSound);
                _itemManager.UpdateItems(new Tuple<string, int, string>(_itemManager.KEY, 1, desc));
                Destroy(gameObject, 1f);
            }
            else if (transform.name.Contains("Chocolate"))
            {
                _audioSource.PlayOneShot(m_ItemSound);
                if (_player.life < 91)
                {
                    _player.RecoverHealth(10);
                }
                else
                {
                    _itemManager.UpdateItems(new Tuple<string, int, string>(_itemManager.CHOCOLATE_BAR, 1, desc));
                }
                Destroy(gameObject, 1f);
            }else if(transform.name.Equals("FX_LightRayRound_01"))
            {
                //_uiManager.saveButton.SetActive(true);
                _uiManager.saveButton.interactable = true;
                _uiManager.index = int.Parse(desc);
                //_hudManager.UpdateInfoHUD("<b><color=#9900FF>Save Point</color></b>\nAccess the menu and select <b>Save</b> to saved your game");
                
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

    public string GetInfoSave()
    {
        return INFO_SAVE;
    }
}
