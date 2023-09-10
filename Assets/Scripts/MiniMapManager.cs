using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapManager : MonoBehaviour
{
    int _minimapLayer;
    private Ray ray;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        _minimapLayer = gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(transform.position, transform.up * -1);

        Debug.DrawRay(transform.position, ray.direction * 2.0f, Color.yellow);

        if (Physics.Raycast(ray, out hit, 2.0f) &&
                hit.collider.gameObject.tag.Equals("Minimap"))
        {
            //Debug.Log(hit.collider.gameObject.name);
            hit.collider.gameObject.layer = gameObject.layer;

        }
    }
}
