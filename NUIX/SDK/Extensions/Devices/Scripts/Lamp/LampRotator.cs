using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampRotator : MonoBehaviour
{
    public GameObject rotatedPart;

    public GameObject middlePart;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void RotateClockwise(float time = 1f)
    {
        StartCoroutine(SmoothRotateClockWise(time));
    }


    public IEnumerator SmoothRotateClockWise(float time)
    { 
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            rotatedPart.transform.RotateAround(middlePart.transform.position, transform.up, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void RotateCounterClockwise(float time = 1f)
    {
        StartCoroutine(SmoothRotateCounterClockWise(time));
    }

    IEnumerator SmoothRotateCounterClockWise(float time)
    {
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            rotatedPart.transform.RotateAround(middlePart.transform.position, -transform.up, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }


}
