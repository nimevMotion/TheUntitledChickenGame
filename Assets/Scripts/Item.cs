using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    public string itemName;
    public int itemSize;
    public string itemDesc;

    private Button _btn;
    private Player _player;
    private ItemManager _itemManager;
    private UIManager _uiManager;

    private void Start()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(TaskOnClick);
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _itemManager = GameObject.Find("UIManager").GetComponent<ItemManager>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    private void TaskOnClick()
    {
        _uiManager.ClickSound();
        switch (itemName)
        {
            case "Butter":
                break;
            case "Chocolate Bar":
                if(_player.life < 90)
                    _player.RecoverHealth(10);
                else
                    _itemManager.UpdateItems(new System.Tuple<string, int, string>(itemName, -1, ""));
                break;
            case "Key":
                Debug.Log(itemDesc);
                Door door = GameObject.Find(itemDesc).GetComponent<Door>();
                if (door != null)
                {
                    door.doorState = Door.DoorState._lock;
                    _itemManager.UpdateItems(new System.Tuple<string, int, string>(itemName, -1, ""));
                }

                break;
            default:
                break;
        }
    }
}
