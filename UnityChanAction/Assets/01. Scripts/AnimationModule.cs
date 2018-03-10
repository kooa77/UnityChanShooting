using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationModule : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Animation

    System.Action _startCallback = null;
    System.Action _midCallback = null;
    System.Action _endCallback = null;

    public void Play(string triggerName, System.Action startCallback,
        System.Action middleCallback,
        System.Action endCallback)
    {
        gameObject.GetComponent<Animator>().SetTrigger(triggerName);
        _startCallback = startCallback;
        _midCallback = middleCallback;
        _endCallback = endCallback;
    }

    public void OnStartAnimation()
    {
        if (null != _startCallback)
            _startCallback();
    }

    public void OnMidAnimation()
    {
        if (null != _midCallback)
            _midCallback();
    }

    public void OnEndAnimation()
    {
        if (null != _endCallback)
            _endCallback();
    }
}
