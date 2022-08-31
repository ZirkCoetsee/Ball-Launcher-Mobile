using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Camera mainCamera;
    private Transform objectTransform;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        objectTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (objectTransform.position.y <= -6)
        {
            Debug.Log("Destroy");
            Destroy(this.gameObject);
        }
    }
}
