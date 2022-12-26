using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    public float lifeTime = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActiveTimer(lifeTime));
    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    IEnumerator ActiveTimer(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
    }
}
