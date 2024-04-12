using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] ParticleSystem _explosionParticle;

    public static ParticleManager Instance;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public void Explode()
    {
        ParticleSystem instantiatedExplosionParticle = Instantiate(_explosionParticle);
        instantiatedExplosionParticle.Play();
        Destroy(instantiatedExplosionParticle.gameObject, instantiatedExplosionParticle.main.startLifetime.constantMax);
    }
}
