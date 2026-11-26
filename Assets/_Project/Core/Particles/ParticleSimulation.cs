using System.Collections.Generic;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

namespace Radiation.Core.Particles
{
    public class ParticleSimulation
    {
        
        public List<Particle> Particles { get; private set; } = new List<Particle>();

        private Emitter _emitter;
        private Random _rng;
        private float _emissionAccumulator;

        
        public float ParticlesPerSecond = 1000f;

        public ParticleSimulation(uint seed)
        {
            _rng = new Random(seed);
            _emitter = new Emitter();
        }

        
        public void SetEmitter(Emitter emitter) => _emitter = emitter;

        
        public void Tick(float deltaTime)
        {
            if (deltaTime <= 0f) return;

            
            _emissionAccumulator += ParticlesPerSecond * deltaTime;
            int spawnCount = (int)_emissionAccumulator;

            if (spawnCount > 0)
            {
                _emissionAccumulator -= spawnCount;
                for (int i = 0; i < spawnCount; i++)
                {
                    Particles.Add(_emitter.CreateParticle(ref _rng));
                }
            }

            
            for (int i = Particles.Count - 1; i >= 0; i--)
            {
                Particle p = Particles[i];

                
                if (p.State != ParticleState.Alive)
                {
                    Particles.RemoveAt(i);
                    continue;
                }

                
                p.Age += deltaTime;

                
                if (p.Age >= p.Lifetime)
                {
                    p.State = ParticleState.Decayed;
                    Particles[i] = p; 
                    continue;
                }

                
                p.Position += p.Velocity * deltaTime;

                
                Particles[i] = p;
            }
        }
    }
}