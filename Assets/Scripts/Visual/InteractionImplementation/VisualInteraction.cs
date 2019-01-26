using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visual.InteractionImplementations
{
    public abstract class VisualInteraction : MonoBehaviour
    {
        public string Id;
        public float Value;
        public float Target;
        public float InterpolateDelta;

        public void InterpolateValue(float deltaTime)
        {
            Value = Target > Value ? Value + InterpolateDelta * deltaTime : Value - InterpolateDelta * deltaTime;
            //TODO nice interpolation
        }

        public virtual void SetValue()
        {
            throw new NotImplementedException("Base class SetValue() cannot be called!");
        }

        private void Update()
        {
            InterpolateValue(Time.deltaTime);
            SetValue();
        }
    }
}
