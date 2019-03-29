using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed, tilt;
    public Boundary boundary;
    public Transform shotSpawn;
    public GameObject shot;
    public float fireRate, startWait;

    private float nextFire = 0, waitTime;

    public SimpleTouchPad touchPad;

    private Quaternion calibrationQuaternion;

    private void Start()
    {
        waitTime = Time.time + startWait;
    }

    private void Update()
    {
            if (Time.time > nextFire && waitTime < Time.time)
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                GetComponent<AudioSource>().Play();
            }
    }

    private void FixedUpdate()
    {
        Vector2 direction = touchPad.GetDirection();
        Vector3 movement = new Vector3(direction.x, 0, direction.y);
        GetComponent<Rigidbody>().velocity = movement*speed;

        GetComponent<Rigidbody>().position = new Vector3(
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            0,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
            );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, 0, GetComponent<Rigidbody>().velocity.x * -tilt);
    }

}
