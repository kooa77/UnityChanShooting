using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItem : MonoBehaviour
{
    void Awake()
    {
        CreateGunModule();
    }

    // Interfaced

    protected GameObject _bulletPrefab;
    protected Character.eGroupType _ownerGroupType;

    protected string _itemID = "default_gun";

    protected float _shotSpeed = 0.1f;

    protected List<GunModule> _gunModuleList = new List<GunModule>();

    virtual protected void CreateGunModule()
    {
        GunModule gunModule = new GunModule();
        gunModule.Init(this);
        _gunModuleList.Add(gunModule);
    }
    
    public void InitGroupType(Character.eGroupType groupType)
    {
        _ownerGroupType = groupType;
    }

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
            for(int i=0; i<_gunModuleList.Count; i++)
            {
                _gunModuleList[i].Fire(startRotation);
            }
        }
    }

    public void CreateBullet(Quaternion startRotation)
    {
        GameObject bulletObject = GameObject.Instantiate(_bulletPrefab, transform.position, startRotation);
        bulletObject.transform.localScale = Vector3.one;

        bulletObject.GetComponent<BulletItem>().SetOwnerGroupType(_ownerGroupType);
    }
}
