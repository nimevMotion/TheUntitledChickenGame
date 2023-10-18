using UnityEngine;

public class DoorSensor : MonoBehaviour
{
    private Door _door;
    
    [SerializeField]
    private bool _isFinal;

    private void Start()
    {
        _door = GetComponentInParent<Door>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag.Equals("Player"))
        {
            _door.isPlayerDetected = true;
            if(_isFinal)
            {
                _door.CanOpenFinalDoor();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _door.isPlayerDetected = false;
    }

}
