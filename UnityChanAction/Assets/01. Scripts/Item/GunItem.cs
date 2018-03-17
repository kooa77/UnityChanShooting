using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItem : MonoBehaviour
{
    // Use this for initialization
	void Start ()
    {
        // struct GunItemAttr
        /*
        GunItemAttr attr =ScriptManager.Instance.FindGunItemAttr(_itemID);
        _shotSpeed = attr.shotSpeed;
        _wayCount = attr.wayCount;
        */
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    // Interfaced

    protected GameObject _bulletPrefab;

    protected string _itemID = "default_gun";
    protected float _shotSpeed = 0.05f;
    protected int _wayCount = 6;

    public void SetBullet(GameObject bulletPrefab)
    {
        _bulletPrefab = bulletPrefab;
    }

    public float GetShotSpeed()
    {
        return _shotSpeed;
    }

    virtual public void Fire(Quaternion startRotation)
    {
        if(null != _bulletPrefab)
        {
            GameObject bulletObject = GameObject.Instantiate(_bulletPrefab, transform.position, startRotation);
            bulletObject.transform.localScale = Vector3.one;
        }
    }
}
