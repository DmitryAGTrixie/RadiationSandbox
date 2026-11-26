using UnityEngine;
using Radiation.Core.Particles;

namespace Radiation.View
{
    public class ParticleRenderer : MonoBehaviour
    {
        public Mesh ParticleMesh;
        public Material ParticleMaterial;
        public float ParticleSize = 0.2f;

        private Matrix4x4[] _matrices;
        private const int BatchSize = 1023; 
        private ParticleSimulation _simulation;

        public void Initialize(ParticleSimulation simulation)
        {
            _simulation = simulation;
            
            _matrices = new Matrix4x4[BatchSize];
        }

        private void LateUpdate()
        {
            if (_simulation == null || ParticleMesh == null || ParticleMaterial == null) return;

            var particles = _simulation.Particles;
            int count = particles.Count;

            
            for (int i = 0; i < count; i += BatchSize)
            {
                int batchCount = Mathf.Min(BatchSize, count - i);

                for (int j = 0; j < batchCount; j++)
                {
                    Particle p = particles[i + j];

                    
                    Vector3 pos = new Vector3(p.Position.x, p.Position.y, p.Position.z);

                    
                    _matrices[j] = Matrix4x4.TRS(pos, Quaternion.identity, Vector3.one * ParticleSize);
                }

                
                Graphics.DrawMeshInstanced(ParticleMesh, 0, ParticleMaterial, _matrices, batchCount);
            }
        }
    }
}