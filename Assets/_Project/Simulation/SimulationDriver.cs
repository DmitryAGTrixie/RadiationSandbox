using UnityEngine;
using Radiation.Core.Particles;
using Radiation.View; 

namespace Radiation.Simulation
{
    public class SimulationDriver : MonoBehaviour
    {
        
        public ParticleRenderer Renderer;

        private ParticleSimulation _simulation;
        private float _logTimer;

        void Start()
        {
            
            _simulation = new ParticleSimulation((uint)System.DateTime.Now.Ticks);

            
            if (Renderer != null)
            {
                Renderer.Initialize(_simulation);
            }
        }

        void Update()
        {
            
            _simulation.Tick(Time.deltaTime);

            
            _logTimer += Time.deltaTime;
            if (_logTimer >= 1f)
            {
                _logTimer = 0f;
                Debug.Log($"Активных частиц в памяти: {_simulation.Particles.Count}");
            }
        }
    }
}