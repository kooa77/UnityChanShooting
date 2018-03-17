using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletItem : MonoBehaviour
{
    float _lifeTime = 3.0f;
    float _moveSpeed = 10.0f;

    // Use this for initialization
    void Start ()
    {
        GameObject.Destroy(gameObject, _lifeTime);
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 moveOffset = _moveSpeed * Vector3.forward;
        transform.position += ((transform.rotation * moveOffset) * Time.deltaTime);

    }
}
