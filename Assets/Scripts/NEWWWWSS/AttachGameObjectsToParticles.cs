using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class AttachGameObjectsToParticles : MonoBehaviour
{
    public GameObject m_Prefab;

    private ParticleSystem m_ParticleSystem;
    private List<Cloth> m_Instances = new List<Cloth>();
    private ParticleSystem.Particle[] m_Particles;

    // Start is called before the first frame update
    void Start()
    {
        m_ParticleSystem = GetComponent<ParticleSystem>();
        m_Particles = new ParticleSystem.Particle[m_ParticleSystem.main.maxParticles];
    }

    // Update is called once per frame
    void LateUpdate()
    {
        int count = m_ParticleSystem.GetParticles(m_Particles);

        while (m_Instances.Count < count)
            m_Instances.Add(Instantiate(m_Prefab, m_ParticleSystem.transform).GetComponent<Cloth>());

        bool worldSpace = (m_ParticleSystem.main.simulationSpace == ParticleSystemSimulationSpace.World);
        for (int i = 0; i < m_Instances.Count; i++)
        {
            if (i < count)
            {
                if (worldSpace)
                {
                    m_Instances[i].transform.position = m_Particles[i].position;
                    m_Instances[i].transform.localEulerAngles = (m_Particles[i].rotation3D);
                    m_Instances[i].ClearTransformMotion();
                }


                else
                {
                    m_Instances[i].transform.localPosition = m_Particles[i].position;
                    m_Instances[i].transform.localEulerAngles = (m_Particles[i].rotation3D);
                    m_Instances[i].ClearTransformMotion();
                }

               
                m_Instances[i].gameObject.SetActive(true);
            }
            else
            {
                m_Instances[i].gameObject.SetActive(false);
            }
        }
    }
}
