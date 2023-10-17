using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{

    public readonly string KEY = "Key";
    public readonly string CHOCOLATE_BAR = "Chocolate Bar";
    public readonly string BUTTER = "Butter"; 

    public List<Tuple<string, int, string>> items = new List<Tuple<string, int, string>>();

    [SerializeField]
    private GameObject m_itemPrefab;
    [SerializeField]
    private Texture[] m_textures;
    [SerializeField]
    private RectTransform rectTransform;

    private int _limWidth;
    private int offsetImg = 164;
    private int offsetItemX = 264;
    private int offsetItemY = 264;

    // Start is called before the first frame update
    void Start()
    {
        items.Add(new Tuple<string, int, string>("Butter", -1, ""));
        DrawItems();
    }

    public void UpdateItems(Tuple<string, int, string> newItem)
    {
        List<Tuple<string, int, string>> temp = new List<Tuple<string, int, string>>();
        bool itemFound = false;
        temp.AddRange(items);
        items.Clear();
        items = new List<Tuple<string, int, string>>();

        for(int i = 0; i < temp.Count; i++)
        {
            if (temp[i].Item1.Equals(newItem.Item1))
            {
                if(temp[i].Item2 + newItem.Item2 >= 1)
                    items.Add(new Tuple<string, int, string>(temp[i].Item1, temp[i].Item2 + newItem.Item2, temp[i].Item3));
                itemFound = true;
            }
            else
                items.Add(temp[i]);
        }

        if (!itemFound)
            items.Add(newItem);

        DrawItems();
    }

    private void DrawItems()
    {
        bool isFirst = true;
        _limWidth = (int)rectTransform.rect.width / 264;
        RectTransform initialRect = null;
        
        GameObject[] itemsTemp = GameObject.FindGameObjectsWithTag("Item");
        foreach(var i in itemsTemp)
        {
            Destroy(i);
        }

        for (int i = 0; i < items.Count; i++)
        {
            GameObject temp;
            TMP_Text tempTxt;
            RawImage tempImg;
            Item tempItem;
            int texInd;
            RectTransform rect;

            switch (items[i].Item1)
            {
                case "Butter":
                    texInd = 1;
                    break;
                case "Chocolate Bar":
                    texInd = 2;
                    break;
                case "Key":
                    texInd= 3; ;
                    break;
                default:
                    texInd = 0;
                    break;
            }

            temp = Instantiate(m_itemPrefab, rectTransform);
            tempItem = temp.GetComponent<Item>();

            if (items[i].Item2 != 0)
            { 
                //Primer 
                if (isFirst)
                {
                    tempItem.itemName = items[i].Item1;
                    tempItem.itemSize = items[i].Item2;
                    tempItem.itemDesc = items[i].Item3;

                    initialRect = temp.GetComponent<RectTransform>();
                    initialRect.anchoredPosition = new Vector2(initialRect.anchoredPosition.x - (rectTransform.rect.width / 2) + offsetImg,
                        initialRect.anchoredPosition.y + (rectTransform.rect.height / 2) - offsetImg);

                    temp.GetComponentInChildren<RawImage>().texture = m_textures[texInd];
                    tempTxt = temp.GetComponentInChildren<TMP_Text>();
                    tempTxt.text = items[i].Item1 + " " + (items[i].Item2 == -1 ? "\u221E" : items[i].Item2);
                    isFirst = false;
                }
                else
                {
                    tempItem.itemName = items[i].Item1;
                    tempItem.itemSize = items[i].Item2;
                    tempItem.itemDesc = items[i].Item3;

                    rect = temp.GetComponent<RectTransform>();
                    rect.anchoredPosition = new Vector2(initialRect.anchoredPosition.x + offsetItemX * ((int)i % _limWidth),
                        initialRect.anchoredPosition.y - offsetItemY * ((int)i / _limWidth));

                    tempImg = temp.GetComponentInChildren<RawImage>();
                    tempImg.texture = m_textures[texInd];

                    tempTxt = temp.GetComponentInChildren<TMP_Text>();
                    tempTxt.text = items[i].Item1 + " " + (items[i].Item2 == -1 ? "\u221E" : items[i].Item2);

                }
            }

        }
    }

    public void UpdateItems(List<Tuple<string, int, string>> newItems)
    {
        items.Clear();
        items.AddRange(newItems);
        DrawItems();
    }
}
