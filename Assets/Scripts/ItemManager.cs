using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{
    //public Item[] items;
    //public GameObject[] items;
    public Tuple<string, int>[] items;
    [SerializeField]
    private GameObject m_itemPrefab;
    [SerializeField]
    private Texture[] m_textures;

    private RectTransform rectTransform;
    private int _limWidth;
    private int _limHeight;
    private int offsetImg = 164;
    private int offsetItemX = 264;
    private int offsetItemY = 264;

    // Start is called before the first frame update
    void Start()
    {
        //items = new Item[2];
        //items[0] = gameObject.AddComponent<Item>();
        //items[0].itemName = "Chocolate Bar";
        //items[0].itemSize = 1;
        //items[1] = gameObject.AddComponent<Item>();
        //items[1].itemName = "Butter";
        //items[1].itemSize = -1;

        items = new Tuple<string, int>[]{
            new Tuple<string, int>("Chocolate Bar", 1),
                new Tuple<string, int>("Butter", -1)
        };

        rectTransform = GetComponent<RectTransform>();
        _limWidth = (int)rectTransform.rect.width / 264;
        _limHeight = (int)rectTransform.rect.height / 264;
        //GameObject temp;
        //GameObject temp2;
        //RawImage tempImg;
        //TMP_Text tempTxt;
        //TMP_Text tempTxt2;

        //temp = Instantiate(m_itemPrefab, rectTransform);
        //RectTransform rect = temp.GetComponent<RectTransform>();
        //Debug.Log(rect.position);
        //rect.anchoredPosition = new Vector2(rect.anchoredPosition.x - (rectTransform.rect.width / 2) +164, 
        //    rect.anchoredPosition.y + (rectTransform.rect.height / 2) - 164);
        ////tempImg = ;
        //temp.GetComponentInChildren<RawImage>().texture = m_textures[0];
        //tempTxt = temp.GetComponentInChildren<TMP_Text>();
        //tempTxt.text = items[1].Name + " " + (items[1].Size == -1 ? "\u221E" : items[1].Size);

        //temp2 = Instantiate(m_itemPrefab, rectTransform);
        //RectTransform rect2 = temp2.GetComponent<RectTransform>();
        //Debug.Log(rect.position);
        //rect2.anchoredPosition = new Vector2(rect.anchoredPosition.x + 264,
        //    rect.anchoredPosition.y);
        ////tempImg = ;
        //temp2.GetComponentInChildren<RawImage>().texture = m_textures[1];
        //tempTxt2 = temp2.GetComponentInChildren<TMP_Text>();
        //tempTxt2.text = items[0].Name + " " + (items[0].Size == -1 ? "\u221E" : items[0].Size);

        RectTransform initialRect = null;
        
        for (int i = 0; i<items.Length;i++)
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
