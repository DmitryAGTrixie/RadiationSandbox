using UnityEngine;
using Radiation.Core.Particles;

namespace Radiation.Simulation
{
    public class SimulationDriver : MonoBehaviour
    {
        private ParticleSimulation _simulation;
        private float _logTimer;

        void Start()
        {
            // Инициализируем ядро. Передаем случайное зерно (seed)
            _simulation = new ParticleSimulation((uint)System.DateTime.Now.Ticks);
        }

        void Update()
        {
            // Каждый кадр "толкаем" ядро, передавая время кадра
            _simulation.Tick(Time.deltaTime);

            // Временный лог для проверки (будет выводить раз в секунду)
            _logTimer += Time.deltaTime;
            if (_logTimer >= 1f)
            {
                _logTimer = 0f;
                Debug.Log($"Активных частиц в памяти: {_simulation.Particles.Count}");
            }
        }
    }
}
