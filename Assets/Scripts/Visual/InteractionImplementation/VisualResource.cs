using System;
using Resources;
using UnityEngine;

namespace Visual.InteractionImplementations
{
    public abstract class VisualResource : MonoBehaviour
    {
        public ResourceType Type;
        public float Value;
        public float Target;
        public float InterpolateDelta;

        public void InterpolateValue(float deltaTime)
        {
            var d = InterpolateDelta * deltaTime;
            if (Target >= Value)
            {
                if (Value + d > Target)
                {
                    Value = Target;
                }
                else
                {
                    Value = Value + d;
                }
            }
            else
            {
                if (Value - d < Target)
                {
                    Value = Target;
                }
                else
                {
                    Value = Value - d;
                }
            }

            Value = Mathf.Clamp(Value, 0f, 1f);
        }

        public virtual void SetValue()
        {
            throw new NotImplementedException("Base class SetValue() cannot be called!");
        }

        private void Update()
        {
            InterpolateValue(Time.deltaTime);
            SetValue();
        }    }
}