using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFactory : MonoBehaviour
{
    [SerializeField] Player _playerPrefab;
    [SerializeField] Transform _playerSpawnPosition;
    [SerializeField] List<Transform> _spawnPositions;
    [SerializeField] List<EnemyData> _enemies;
    [SerializeField] HUD _hud;
    [SerializeField] Progression _progression;
    [SerializeField] CinemachineVirtualCamera _camera;

    private List<EnemyBrain> _spawnedEnemies = new List<EnemyBrain>();
    private Player _player;

    private void Start()
    {
        CreatePlayer();
        _camera.Follow = _player.transform;
        _hud.Init(_player);
        RestorePlayer();
        StartCoroutine(SpawnEnemies());
    }

    private void CreatePlayer()
    {
        _player = Instantiate(_playerPrefab, _playerSpawnPosition.position, Quaternion.identity);
        _player.Init(StaticData.SelectedRole, _progression);
    }

    private void RestorePlayer()
    {
        if (PlayerPrefs.HasKey("player"))
        {
            string json = PlayerPrefs.GetString("player");
            Debug.Log(json);
            var data = JsonUtility.FromJson<PlayerData>(json);
            _player.Restore(data);
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(4);
            SpawnRandomEnemy();
        }
    }

    private void SpawnRandomEnemy()
    {
        var posIndex = Random.Range(0, _spawnPositions.Count);
        var enemyIndex = Random.Range(0, _enemies.Count);

        CreateEnemyAt(_enemies[enemyIndex], _spawnPositions[posIndex].position);
    }

    private void CreateEnemyAt(EnemyData data, Vector3 position)
    {
        var enemy = Instantiate(data.Prefab, position, Quaternion.identity);
        _spawnedEnemies.Add(enemy);
        enemy.Init(_player, data);
        enemy.OnDied += OnDied;
    }

    private void OnDied(EnemyBrain enemy)
    {
        _player.BoostExp(enemy.ExpReward);
        _player.BoostGold(enemy.GoldReward);
    }

    private void OnApplicationQuit()
    {
        var data = _player.Save();
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("player", json);
    }
}
