using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletItem : MonoBehaviour
{
    public float MoveSpeed = 10.0f;

    protected Character.eGroupType _ownerGroupType;

    protected float _lifeTime = 20.0f;
    protected float _moveSpeed = 1.0f;
    protected Character _target = null;

    void Start ()
    {
        GameObject.Destroy(gameObject, _lifeTime);
        _moveSpeed = MoveSpeed;
    }
	
	void Update ()
    {
        Vector3 moveOffset = _moveSpeed * Vector3.forward;
        transform.position += ((transform.rotation * moveOffset) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Character colliderCharacter = other.gameObject.GetComponent<Character>();
        if(null != colliderCharacter)
        {
            if(colliderCharacter.GetGroupType() != _ownerGroupType)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }

    public void SetOwnerGroupType(Character.eGroupType ownerGroupType)
    {
        _ownerGroupType = ownerGroupType;
    }

    public void SetTarget(Character target)
    {
        _target = target;
    }
}
