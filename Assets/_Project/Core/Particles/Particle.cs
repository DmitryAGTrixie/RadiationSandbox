using Unity.Mathematics;

namespace Radiation.Core.Particles
{
    public enum ParticleState : byte
    {
        Alive = 0,
        Absorbed = 1,
        Escaped = 2,
        Decayed = 3
    }

    public struct Particle
    {
        public float3 Position;
        public float3 Velocity;
        public float Energy;
        public float Age;
        public float Lifetime;
        public ParticleState State;

        public bool IsAlive => State == ParticleState.Alive;
    }
}