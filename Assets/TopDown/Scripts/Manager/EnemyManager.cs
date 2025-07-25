using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownSession
{
    public class EnemyManager : MonoBehaviour
    {
        private Coroutine waveRoutine;

        [SerializeField]
        private List<GameObject> enemyPrefabs; // 생성할 적 프리팹 리스트

        [SerializeField]
        private List<Rect> spawnAreas; // 적을 생성할 영역 리스트

        [SerializeField]
        private Color gizmoColor = new Color(1, 0, 0, 0.3f); // 기즈모 색상

        private List<EnemyController> activeEnemies = new List<EnemyController>(); // 현재 활성화된 적들

        private bool enemySpawnComplite;

        [SerializeField] private float timeBetweenSpawns = 0.2f;
        [SerializeField] private float timeBetweenWaves = 1f;

        GameManager gameManager;
        public void Init(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void StartWave(int waveCount)
        {
            if (waveCount <= 0)
            {
                gameManager.EndOfWave();
                return;
            }
            if (waveRoutine != null)
                StopCoroutine(waveRoutine);
            waveRoutine = StartCoroutine(SpawnWave(waveCount));
        }

        public void StopWave()
        {
            StopAllCoroutines();
        }

        private IEnumerator SpawnWave(int waveCount)
        {
            enemySpawnComplite = false;
            yield return new WaitForSeconds(timeBetweenWaves);
            for (int i = 0; i < waveCount; i++)
            {
                yield return new WaitForSeconds(timeBetweenSpawns);
                SpawnRandomEnemy();
            }

            enemySpawnComplite = true;
        }

        private void SpawnRandomEnemy()
        {
            if (enemyPrefabs.Count == 0 || spawnAreas.Count == 0)
            {
                Debug.LogWarning("Enemy Prefabs 또는 Spawn Areas가 설정되지 않았습니다.");
                return;
            }

            // 랜덤한 적 프리팹 선택
            GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

            // 랜덤한 영역 선택
            Rect randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];

            // Rect 영역 내부의 랜덤 위치 계산
            Vector2 randomPosition = new Vector2(
                Random.Range(randomArea.xMin, randomArea.xMax),
                Random.Range(randomArea.yMin, randomArea.yMax)
            );

            // 적 생성 및 리스트에 추가
            GameObject spawnedEnemy = Instantiate(randomPrefab, new Vector3(randomPosition.x, randomPosition.y), Quaternion.identity);
            EnemyController enemyController = spawnedEnemy.GetComponent<EnemyController>();
            enemyController.Init(this, gameManager.player.transform);

            activeEnemies.Add(enemyController);
        }
        public void RemoveEnemyOnDeath(EnemyController enemy)
        {
            activeEnemies.Remove(enemy);
            if (enemySpawnComplite && activeEnemies.Count == 0)
                gameManager.EndOfWave();
        }

        // 기즈모를 그려 영역을 시각화 (선택된 경우에만 표시)
        // 개발을 위한 아이콘
        private void OnDrawGizmosSelected()
        {
            if (spawnAreas == null) return;

            Gizmos.color = gizmoColor;
            foreach (var area in spawnAreas)
            {
                Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
                Vector3 size = new Vector3(area.width, area.height);
                Gizmos.DrawCube(center, size);
            }
        }
    }
}