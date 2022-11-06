using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnSettings", menuName = "EasyCodeForVivox/3D Demo Scene/SpawnSettings")]
public class SpawnSettingsSO : ScriptableObject
{
    [SerializeField] private List<Vector3> _spawnTransformList = new List<Vector3>();
    private System.Random _random = new System.Random();

    public Vector3 GetSpawnPoint()
    {
        if (_spawnTransformList.Count > 0)
        {
            return _spawnTransformList[_random.Next(_spawnTransformList.Count - 1)];
        }
        return Vector3.zero;
    }
}
