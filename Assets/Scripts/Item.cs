using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    public string itemName;
    public int itemSize;

    private Button _btn;
    private TMP_Text _tmpText;

    private Player _player;

    private void Start()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(TaskOnClick);
        _tmpText = GetComponentInChildren<TMP_Text>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    private void TaskOnClick()
    {
        Debug.Log("You have clicked the button!" + _tmpText.text);
        switch (itemName)
        {
            case "Butter":
                break;
            case "Chocolate Bar":
                _player.RecoverHealth(10);
                itemSize =itemSize - 1;
                _tmpText.text = itemName + " " + itemSize.ToString();
                break;
            default:
                break;
        }
    }
}
