using UnityEngine;

namespace Dynamic
{
    public class AnimalCharacter : DynamicObject
    {
        public AnimalCharacter(Vector3 position, Graph.Graph graph) : base(position, graph)
        {
        }

        public override void DynamicUpdate()
        {
            base.DynamicUpdate();
        }
    }
}