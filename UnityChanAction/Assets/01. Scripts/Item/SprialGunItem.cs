using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprialGunItem : GunItem
{
    override protected void CreateGunModule()
    {
        {
            GunModule gunModule = new SpiralGunModule();
            gunModule._wayCount = 5;
            gunModule._rotateY = -1.0f;
            gunModule.Init(this);
            _gunModuleList.Add(gunModule);
        }
        {
            GunModule gunModule = new SpiralGunModule();
            gunModule._wayCount = 2;
            gunModule._rotateY = 10.0f;
            gunModule.Init(this);
            _gunModuleList.Add(gunModule);
        }
    }
}
