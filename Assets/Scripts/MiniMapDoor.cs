using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapDoor : MonoBehaviour
{
    [SerializeField]
    private Door m_Door;
    [SerializeField]
    private Material m_UnlockMat;
    [SerializeField]
    private Material m_LockMat;

    int layerMinimap;

    // Start is called before the first frame update
    void Start()
    {
        layerMinimap = LayerMask.NameToLayer("Map");
    }

    // Update is called once per frame
    void Update()
    {
        switch(m_Door.doorState)
        {
            case 1:
                gameObject.layer = layerMinimap;
                break;
                            
            case 2:
                gameObject.GetComponent<MeshRenderer>().material = m_UnlockMat;
                break;
            
            case 3:
                gameObject.GetComponent<MeshRenderer>().material = m_LockMat;
                break;
            
            default:
                break;
        }
    }
}
