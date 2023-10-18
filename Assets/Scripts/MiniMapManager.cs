using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_MiniMap;

    private Ray ray;
    private RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(transform.position, transform.up * -1);

        Debug.DrawRay(transform.position, ray.direction * 2.0f, Color.yellow);

        if (Physics.Raycast(ray, out hit, 2.0f) &&
                hit.collider.gameObject.tag.Equals("Minimap") &&
                hit.collider.gameObject.layer != gameObject.layer)
        {
            hit.collider.gameObject.layer = gameObject.layer;
        }
    }

    public List<Tuple<string, bool>> GetData()
    {
        List<Tuple<string, bool>> map = new List<Tuple<string, bool>>();
        foreach (Transform child in m_MiniMap.transform)
        {
            bool isVisble = child.gameObject.layer == gameObject.layer ? true : false;
            map.Add(new Tuple<string, bool>(child.name, isVisble));
        }
        return map;
    }

    public void UpdateMapa(List<Tuple<string, bool>> minimap)
    {
        Transform[] goList = gameObject.GetComponentsInChildren<Transform>();
        
        foreach(var i in minimap)
            foreach (Transform child in m_MiniMap.transform)
            {
                if (child.name.Equals(i.Item1))
                {
                    if (i.Item2)
                    {
                        child.gameObject.layer = gameObject.layer;
                        break;
                    }
                }
            }
    }
}
