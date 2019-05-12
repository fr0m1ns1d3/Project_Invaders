using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldActivator : MonoBehaviour
{
    [SerializeField]
    private AudioClip powerSound;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Shield"))
        {
            Destroy(this.gameObject);
            return;
        }

        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                AudioSource.PlayClipAtPoint(powerSound, Camera.main.transform.position, 1f);
                player.ShieldOn();
                Destroy(this.gameObject);
            }
        }
        else return;
    }
}
