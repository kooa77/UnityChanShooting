using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItem : MonoBehaviour
{
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    // Interfaced

    GameObject _bulletPrefab;

    float _shotSpeed = 0.2f;

    public void SetBullet(GameObject bulletPrefab)
    {
        _bulletPrefab = bulletPrefab;
    }

    public float GetShotSpeed()
    {
        return _shotSpeed;
    }

    public void Fire(Quaternion startRotation)
    {
        if(null != _bulletPrefab)
        {
            GameObject bulletObject = GameObject.Instantiate(_bulletPrefab, transform.position, startRotation);
            bulletObject.transform.localScale = Vector3.one;
        }
    }
}
