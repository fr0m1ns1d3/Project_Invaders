using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manevrs : MonoBehaviour
{
    public Vector2 startWait, manTime, manWait;
    public float dodge, smoothing, tilt;
    public Boundary boundary;

    private float currentSpeed;
    private float targetManv;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Evade());
        currentSpeed = -5;
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {
            targetManv = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(manTime.x, manTime.y));
            targetManv = 0;
            yield return new WaitForSeconds(Random.Range(manWait.x, manWait.y));
        }
    }

    void FixedUpdate()
    {
        float newManv = Mathf.MoveTowards(rb.velocity.x, targetManv, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManv, 0, currentSpeed);
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );
        rb.rotation = Quaternion.Euler(0, 180, rb.velocity.x * tilt);
    }
}
