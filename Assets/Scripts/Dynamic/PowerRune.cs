using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dynamic
{
    public class PowerRune : DynamicObject
    {
        public PowerRune(Vector3 position):base(position)
        {
            this.prefab = UnityEngine.Resources.Load<GameObject>("Stone");
        }

        override public DynamicObject PickUpObject()
        {
            if (this.GetHeldByPlayer() == false)
            {
                this.SetHeldByPlayer(true);
                return this;
            }
            return null;
        }
    }
}
