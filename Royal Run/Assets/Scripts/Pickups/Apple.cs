using UnityEngine;

public class Apple : Pickup
{

    #region levelGenerator
    LevelGenerator levelGenerator;
    [SerializeField] float adjustChangeMoveSpeedAmount = 3f;
    #endregion

    public void Init(LevelGenerator lg) {
        this.levelGenerator = lg;
    }

    protected override void OnPickup(){
        levelGenerator.ChangeChunkMoveSpeed(adjustChangeMoveSpeedAmount);
    }
}
