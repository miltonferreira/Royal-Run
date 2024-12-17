using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;

    const string playerString = "Player";

    void Update() {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);    
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag(playerString)){
            OnPickup();
            Destroy(this.gameObject); 
        }
        
    }

    protected abstract void OnPickup();
}