using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{
    public Item[] item;

    [SerializeField]
    private GameObject m_itemPrefab;
    [SerializeField]
    private Texture[] m_textures;

    private RectTransform rectTransform;
    private int _limWidth;
    private int _limHeight;

    // Start is called before the first frame update
    void Start()
    {
        item = new Item[2];
        item[0] = new Item("Chocolate Bar", 1);
        item[1] = new Item("Butter", -1);
        rectTransform = GetComponent<RectTransform>();
        Debug.Log(rectTransform.rect.width);
        _limWidth = (int)rectTransform.rect.width / 264;
        _limHeight = (int)rectTransform.rect.height / 264;
        GameObject temp;
        GameObject temp2;
        RawImage tempImg;
        TMP_Text tempTxt;
        TMP_Text tempTxt2;

        temp = Instantiate(m_itemPrefab, rectTransform);
        RectTransform rect = temp.GetComponent<RectTransform>();
        Debug.Log(rect.position);
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x - (rectTransform.rect.width / 2) +164, 
            rect.anchoredPosition.y + (rectTransform.rect.height / 2) - 164);
        //tempImg = ;
        temp.GetComponentInChildren<RawImage>().texture = m_textures[0];
        tempTxt = temp.GetComponentInChildren<TMP_Text>();
        tempTxt.text = item[1].Name + " " + (item[1].Size == -1 ? "\u221E" : item[1].Size);

        temp2 = Instantiate(m_itemPrefab, rectTransform);
        RectTransform rect2 = temp2.GetComponent<RectTransform>();
        Debug.Log(rect.position);
        rect2.anchoredPosition = new Vector2(rect.anchoredPosition.x + 264,
            rect.anchoredPosition.y);
        //tempImg = ;
        temp2.GetComponentInChildren<RawImage>().texture = m_textures[1];
        tempTxt2 = temp2.GetComponentInChildren<TMP_Text>();
        tempTxt2.text = item[0].Name + " " + (item[0].Size == -1 ? "\u221E" : item[0].Size);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
