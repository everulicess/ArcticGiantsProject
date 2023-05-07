using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovement : MonoBehaviour
{
    float movementSpeed = 9f;
    float rotationSpeed = 75f;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWandering == false)
        {
            StartCoroutine(Wander());
        }

        if (isRotatingRight == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotationSpeed);
        }
        if (isRotatingLeft == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotationSpeed);
        }

        if (isWalking)
        {
            rb.AddForce(transform.forward * movementSpeed);
        }
    }

    IEnumerator Wander()
    {
        int rotationTime = Random.Range(1, 2);
        int rotateWait = Random.Range(1, 3);
        int rotateDirection = Random.Range(1, 2);
        int walkTime = Random.Range(1, 3);
        int walkWait = Random.Range(1, 3);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);

        isWalking = true;

        yield return new WaitForSeconds(walkTime);

        isWalking = false;

        yield return new WaitForSeconds(rotateWait);

        if (rotateDirection == 1)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingLeft = false;
        }

        if (rotateDirection == 2)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingRight = false;
        }

        isWandering = false;
    }
}
