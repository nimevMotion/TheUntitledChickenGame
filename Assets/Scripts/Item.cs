using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    public string itemName;
    public int itemSize;
    public string itemDesc;

    private Button _btn;
    private TMP_Text _tmpText;

    private Player _player;
    private ItemManager _itemManager;

    private void Start()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(TaskOnClick);
        _tmpText = GetComponentInChildren<TMP_Text>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _itemManager = GameObject.Find("UIManager").GetComponent<ItemManager>();

    }

    private void TaskOnClick()
    { 
        switch (itemName)
        {
            case "Butter":
                Debug.Log("Soy mantequilla");
                break;
            case "Chocolate Bar":
                _player.RecoverHealth(10);
                //itemSize =itemSize - 1;
                //_tmpText.text = itemName + " " + itemSize.ToString();
                _itemManager.UpdateItems(new System.Tuple<string, int, string>(itemName, itemSize - 1, ""));
                break;
            case "Key":
                Debug.Log(itemDesc);
                Door door = GameObject.Find(itemDesc).GetComponent<Door>();
                if (door != null)
                {
                    door.doorState = Door.DoorState._lock;
                    _itemManager.UpdateItems(new System.Tuple<string, int, string>(itemName, 0, ""));
                }

                break;
            default:
                break;
        }
    }
}
