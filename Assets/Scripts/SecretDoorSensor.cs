using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretDoorSensor : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    private Door _door;
    private HUDManager _hudManager;

    // Start is called before the first frame update
    void Start()
    {
        _door = GetComponentInParent<Door>();
        _hudManager = GameObject.Find("UIManager").GetComponent<HUDManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            _hudManager.UpdateInfoHUD("Flush\nPress Q");
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _hudManager.UpdateInfoHUD("Flush\nPress Q");
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine("OpenSecretDoor");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _hudManager.DeactivateInfoHUD();
        }
    }

    IEnumerator OpenSecretDoor()
    {
        _audioSource.Play();
        yield return new WaitForSeconds(2.5f);
        _door.doorState = Door.DoorState.unlock;
        _door.isPlayerDetected = true;
        _door.isOpen = true;
        _hudManager.DeactivateInfoHUD();
        Destroy(gameObject);
    }

}
