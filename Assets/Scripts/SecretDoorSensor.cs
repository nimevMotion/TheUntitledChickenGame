using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretDoorSensor : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_audioSource;
    [SerializeField]
    private GameObject m_Ghost;

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
        m_audioSource.Play();
        if(m_Ghost != null)
        {
            GhostManager manager = m_Ghost.GetComponent<GhostManager>();
            manager.state = GhostManager.GhostState.spooky;
        }
        yield return new WaitForSeconds(2.5f);
        _door.doorState = Door.DoorState.unlock;
        _door.isPlayerDetected = true;
        _door.isOpen = true;
        _hudManager.DeactivateInfoHUD();
        Destroy(gameObject);
    }

}
