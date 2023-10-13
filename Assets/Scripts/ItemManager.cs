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
        Debug.Log("Inicia la actualizacion de objetos");
        List<Tuple<string, int, string>> temp = new List<Tuple<string, int, string>>();
        temp.AddRange(items);
        items.Clear();
        Debug.Log("a:" + temp.Count);
        foreach(var i in temp)
        {
            if(i.Item1.Equals(newItem.Item1))
            {
                items.Add(newItem);
                Debug.Log("Se actualiza item:" + newItem.Item1);
            }
            else
            {
                items.Add(i);
                Debug.Log("Se encontro un nuevo item:" + newItem.Item1);
            }
        }
        Debug.Log("b:" + items.Count);
        DrawItems();
    }

    private void DrawItems()
    {
        Debug.Log(items.Count);
        _limWidth = (int)rectTransform.rect.width / 264;
        RectTransform initialRect = null;

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
            //Primer 
            if (i == 0)
            {
                temp = Instantiate(m_itemPrefab, rectTransform);
                tempItem = temp.GetComponent<Item>();
                tempItem.itemName = items[i].Item1;
                tempItem.itemSize = items[i].Item2;

                initialRect = temp.GetComponent<RectTransform>();
                initialRect.anchoredPosition = new Vector2(initialRect.anchoredPosition.x - (rectTransform.rect.width / 2) + offsetImg,
                    initialRect.anchoredPosition.y + (rectTransform.rect.height / 2) - offsetImg);

                temp.GetComponentInChildren<RawImage>().texture = m_textures[texInd];
                tempTxt = temp.GetComponentInChildren<TMP_Text>();
                tempTxt.text = items[i].Item1 + " " + (items[i].Item2 == -1 ? "\u221E" : items[i].Item2);
            }
            else
            {
                temp = Instantiate(m_itemPrefab, rectTransform);
                tempItem = temp.GetComponent<Item>();
                tempItem.itemName = items[i].Item1;
                tempItem.itemSize = items[i].Item2;

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
