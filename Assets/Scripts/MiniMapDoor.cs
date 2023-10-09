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
        switch (m_Door.doorState)
        {
            case Door.DoorState.undiscovered:
                gameObject.layer = layerMinimap;
                break;

            case Door.DoorState.unlock:
                gameObject.GetComponent<MeshRenderer>().material = m_UnlockMat;
                break;

            case Door.DoorState.block:
                gameObject.GetComponent<MeshRenderer>().material = m_LockMat;
                break;

            default:
                break;
        }
    }
}
