using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Traking : MonoBehaviour
{
    public float offset;
    [SerializeField] private Guns guns;
    [SerializeField] private PlayerScript2D player;

    void Update()
    {
        if (guns.isRealoding)
        {
            transform.localScale = player.transform.localScale;
            transform.rotation = Quaternion.Euler(180 * player.transform.rotation.y, 0f, 180f * player.transform.rotation.y);
            return;
        }
        Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f , rotateZ + offset);

        Vector3 LocalScale = Vector3.one;

        if (rotateZ > 90 || rotateZ < -90)
        {
            LocalScale.y = -1f;
        }
        else
        {
            LocalScale.x = +1f;
        }
        transform.localScale = LocalScale;
    }
}
