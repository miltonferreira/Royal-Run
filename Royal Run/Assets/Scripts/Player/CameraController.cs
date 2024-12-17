using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour {

    CinemachineCamera cinemachineCamera;
    [SerializeField] ParticleSystem speedupParticleSystem;

    [SerializeField] float minFOV = 20f;
    [SerializeField] float maxFOV = 120f;
    [SerializeField] float zoomDuration = 1f;
    [SerializeField] float zoomSpeedModifer = 5f;

    void Awake() {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    public void ChangeCameraFOV(float speedAmount){
        StopAllCoroutines();
        StartCoroutine(ChangeFOVRoutine(speedAmount));

        if(speedAmount > 0){
            speedupParticleSystem.Play();
        }
    }

    IEnumerator ChangeFOVRoutine(float speedAmount){
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float targetFOV = Mathf.Clamp((startFOV + speedAmount  * zoomSpeedModifer), minFOV, maxFOV);

        float elapsedTime = 0f;

        while(elapsedTime < zoomDuration){
            float t = 
            elapsedTime += Time.deltaTime;
            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, t);
            yield return null;
        }
        
        cinemachineCamera.Lens.FieldOfView = targetFOV;
    }
}