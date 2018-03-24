using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletItem : MonoBehaviour
{
    public float MoveSpeed = 10.0f;

    Character.eGroupType _ownerGroupType;
    float _lifeTime = 20.0f;
    float _moveSpeed = 1.0f;

    // Use this for initialization
    void Start ()
    {
        GameObject.Destroy(gameObject, _lifeTime);
        _moveSpeed = MoveSpeed;
    }
	
	// Update is called once per frame
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
}
