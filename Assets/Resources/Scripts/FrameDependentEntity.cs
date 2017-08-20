using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FrameDependentEntity : MonoBehaviour {

    protected virtual void Start()
    {
        TimeManager.instance.RegisterEntity(this);
    }

    public abstract void UpdateFrame();

}
