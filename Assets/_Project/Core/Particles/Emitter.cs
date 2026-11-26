using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

namespace Radiation.Core.Particles
{
    public class Emitter
    {
        // Параметры источника
        public float3 Position = float3.zero;      
        public float3 Direction = new float3(0, 0, 1); 
        public EmissionMode Mode = EmissionMode.Omnidirectional;

        public float ConeAngle = 30f;              
        public float MinSpeed = 5f;                
        public float MaxSpeed = 10f;               
        public float MinLifetime = 2f;             
        public float MaxLifetime = 5f;             
        public float StartEnergy = 100f;           

        
        public Particle CreateParticle(ref Random rng)
        {
            float3 velocityDir;

            
            switch (Mode)
            {
                case EmissionMode.Beam:
                    velocityDir = Direction;
                    break;

                case EmissionMode.Cone:
                    
                    float3 randomDir = rng.NextFloat3Direction();
                    
                    velocityDir = math.lerp(Direction, randomDir, ConeAngle / 180f);
                    break;

                case EmissionMode.Omnidirectional:
                default:
                    velocityDir = rng.NextFloat3Direction();
                    break;
            }

            
            return new Particle
            {
                Position = Position,
                Velocity = velocityDir * rng.NextFloat(MinSpeed, MaxSpeed),
                Energy = StartEnergy,
                Age = 0f,
                Lifetime = rng.NextFloat(MinLifetime, MaxLifetime), 
                State = ParticleState.Alive
            };
        }
    }
}
