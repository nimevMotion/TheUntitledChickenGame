using UnityEngine;

public class DoorSensor : MonoBehaviour
{
    private Door _door;

    private void Start()
    {
        _door = GetComponentInParent<Door>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag.Equals("Player"))
        {
            _door.isPlayerDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _door.isPlayerDetected = false;
    }

}
