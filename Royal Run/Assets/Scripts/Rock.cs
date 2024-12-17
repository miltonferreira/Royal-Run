using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class Rock : MonoBehaviour {

    [SerializeField] ParticleSystem collisionParticleSystem;
    [SerializeField] AudioSource boulderSmashAudioSource;
    [SerializeField] float shakeModifer = 10f;
    
    # region Timer
    [SerializeField] float collisionCooldown = 1f;
    float collisionTimer = 1f;
    #endregion

    CinemachineImpulseSource cinemachineImpulseSource;
    
    void Awake(){
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update() {
        collisionTimer += Time.deltaTime;    
    }

    void OnCollisionEnter(Collision other)
    {

        if (collisionTimer < collisionCooldown) return;

        FireImpulse();
        CollisionFX(other);
        collisionTimer = 0f;
    }

    void FireImpulse(){
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = (1f / distance) * shakeModifer;
        shakeIntensity = Mathf.Min(shakeIntensity, 1f);

        cinemachineImpulseSource.GenerateImpulse(shakeIntensity);
    }

    void CollisionFX(Collision other){
        ContactPoint contactPoint = other.contacts[0];
        collisionParticleSystem.transform.position = contactPoint.point;
        collisionParticleSystem.Play();
        boulderSmashAudioSource.Play();
    }

}
