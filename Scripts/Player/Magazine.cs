using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    public float destroy = 5f;
    void Start()
    {
        Invoke("destroyMag", destroy);
    }


    private void destroyMag()
    {
        Destroy(gameObject);
    }
}
