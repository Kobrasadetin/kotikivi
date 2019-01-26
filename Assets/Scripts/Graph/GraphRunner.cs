using UnityEngine;

namespace Graph
{
    [RequireComponent(typeof(GraphSingleton))]
    public class GraphRunner : MonoBehaviour
    {
        public float TickRate;

        public static GraphRunner Instance { get; private set; }

        public float CurrentTick;

        private float tickCounter = 0f;

        public void Awake()
        {
            Instance = this;
            CurrentTick = 0f;
        }

        public void Update()
        {
            tickCounter += Time.deltaTime * TickRate;

            int nTicksToRun = Mathf.FloorToInt(tickCounter);
            for (int i = 0; i < nTicksToRun; i++)
            {
                GetComponent<GraphSingleton>().Graph.Tick();
                CurrentTick++;
            }

            tickCounter -= nTicksToRun;
        }
    }
}