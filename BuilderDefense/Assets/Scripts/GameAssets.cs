using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _instance;

    public static GameAssets Instance
    {
        get
        {
            if (!_instance) _instance = Resources.Load<GameAssets>(nameof(GameAssets));
            return _instance;
        }
    }

    public Transform pfEnemy;
    public Transform pfArrowProjectile;
    public Transform pfBuildingDestroyedParticles;
    public Transform pfBuildingConstruction;
    public Transform pfBuildingPlacedParticles;
    public Transform pfEnemyDieParticles;
}