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

    private Vector3 touchPosition;
    private Rigidbody rb;
    private Vector3 direction;

    public float speed, tilt;
    public Boundary boundary;
    public Transform shotSpawn;
    public GameObject shot;
    public float fireRate, startWait;

    private float nextFire = 0, waitTime;

    [SerializeField]
    private GameObject playerShield;

    private Quaternion calibrationQuaternion;

    private void Start()
    {
        waitTime = Time.time + startWait;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Time.time > nextFire && waitTime < Time.time)
        {
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                GetComponent<AudioSource>().Play();
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.y = 0;
            direction = (touchPosition - transform.position);
            rb.velocity = new Vector3(direction.x, direction.y, direction.z) * speed;

            if (touch.phase == TouchPhase.Ended)
                rb.velocity = Vector3.zero;

            if (transform.position.x > boundary.xMax)
            {
                transform.position = new Vector3(boundary.xMax, 0, transform.position.z);
            }
            else if (transform.position.x < boundary.xMin)
            {
                transform.position = new Vector3(boundary.xMin, 0, transform.position.z);
            }

            if (transform.position.z > boundary.zMax)
            {
                transform.position = new Vector3(transform.position.x, 0, boundary.zMax);
            }
            else if (transform.position.z < boundary.zMin)
            {
                transform.position = new Vector3(transform.position.x, 0, boundary.zMin);
            }
        }
    }

    private void FixedUpdate()
    {      
        rb.rotation = Quaternion.Euler(0, 0, GetComponent<Rigidbody>().velocity.x * -tilt);
    }

    public void ShieldOn ()
    {
        playerShield.SetActive(true);
        StartCoroutine(ShieldOff());
    } 

    IEnumerator ShieldOff()
    {
        yield return new WaitForSeconds(10);
        playerShield.SetActive(false);
    }
}
