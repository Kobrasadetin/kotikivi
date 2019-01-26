using System;
using Interactions;
using Resources;
using UnityEngine;

namespace Visual.InteractionImplementations
{
    public class VisualStream : MonoBehaviour
    {
        public ResourceType Type;
        public InterActionType IType;
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