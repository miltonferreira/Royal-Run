using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] float collisionCooldown = 1f;
    float cooldownTimer = 0f;

    #region levelGenerator
    LevelGenerator levelGenerator;
    [SerializeField] float adjustChangeMoveSpeedAmount = -2f;
    #endregion

    [SerializeField] Animator animator;
    const string StumbleAnimation = "Stumble";

    void Start() {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }

    void Update() {
        cooldownTimer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other) {

        if(cooldownTimer < collisionCooldown) return;

        levelGenerator.ChangeChunkMoveSpeed(adjustChangeMoveSpeedAmount);

        animator.SetTrigger(StumbleAnimation);
        cooldownTimer = 0f;
    }
}