using UnityEngine;
using System.Collections;

public class Ethereal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public ParticleSystem ParticleSys;
    
    void Start()
    {
        Instantiate(ParticleSys, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
