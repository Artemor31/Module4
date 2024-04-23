using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFactory : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private List<Enemy> _enemiesData;
    [Header("Player")]
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Progression _progression;
    [Header("Level")]
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private HUD _hud;

    private Player _player;
    private List<EnemyBrain> _enemies = new List<EnemyBrain>();
    SaveManager _saveManager;

    private void Start()
    {
        _saveManager = new SaveManager();
        _player = CreatePlayer();
        _hud.Init(_player);
        _camera.Follow = _player.transform;

        _saveManager.RestorePlayer(_player);
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(4);

            var i = Random.Range(0, _enemiesData.Count);
            var j = Random.Range(0, _spawnPoints.Count);

            SpawnEnemy(_enemiesData[i], _spawnPoints[j].position);
        }
    }

    private void SpawnEnemy(Enemy data, Vector3 position)
    {
        var enemy = Instantiate(data.Prefab, position, Quaternion.identity);
        enemy.Init(_player, data);
        enemy.OnDied += EnemyOnDied;
        _enemies.Add(enemy);
    }

    private void EnemyOnDied(int gold, int exp, EnemyBrain enemy)
    {
        _player.BoostGold(gold);
        _player.BoostExp(exp);
        enemy.OnDied -= EnemyOnDied;
    }

    private Player CreatePlayer()
    {
        var player = Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
        player.Init(StaticData.SelectedRole, _camera.GetComponent<Camera>(), _progression);
        return player;
    }

    private void OnApplicationQuit()
    {
        _saveManager.SavePlayer(_player);
    }
}
