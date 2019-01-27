using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dynamic
{
    public class PlayerCharacter : DynamicObject
    {
        public PlayerCharacter(Vector3 position) : base(position)
        {
            this.prefab = UnityEngine.Resources.Load<GameObject>("Cat/Player");
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
