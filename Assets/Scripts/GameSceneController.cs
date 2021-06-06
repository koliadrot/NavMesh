using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameSceneController : MonoBehaviour
{
    public static GameSceneController instance;

    public GameObject Player { get; set; }

    [SerializeField] private LevelGenerator levelGenerator = new LevelGenerator();

    [System.Serializable]
    private class LevelGenerator
    {
        public NavMeshSurface surface;

        [Header("Size")]
        public int width = 10;
        public int height = 10;


        [Header("Prefabs")]
        public GameObject wall;
        public GameObject player;
        public GameObject enemy;

        private bool playerSpawned = false;
        private bool enemySpawned = false;

        // Create a grid based level
        public void GenerateLevel()
        {
            // Loop over the grid
            for (int x = 0; x <= width; x += 2)
            {
                for (int y = 0; y <= height; y += 2)
                {
                    // Should we place a wall?
                    if (Random.value > .7f)
                    {
                        // Spawn a wall
                        Vector3 pos = new Vector3(x - width / 2f, 1f, y - height / 2f);
                        Instantiate(wall, pos, Quaternion.identity, GameSceneController.instance.transform);
                    }
                    else if (!playerSpawned) // Should we spawn a player?
                    {
                        // Spawn the player
                        Vector3 pos = new Vector3(x - width / 2f, 1.25f, y - height / 2f);
                        GameSceneController.instance.Player = Instantiate(player, pos, Quaternion.identity);
                        playerSpawned = true;
                    }
                    else if (!enemySpawned) // Should we spawn a enemy?
                    {
                        // Spawn the enemy
                        Vector3 pos = new Vector3(x - width / 2f, 1.25f, y - height / 2f);
                        Instantiate(enemy, pos, Quaternion.identity);
                        enemySpawned = true;
                    }
                }
            }
            surface.BuildNavMesh();
        }
    }

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        levelGenerator.GenerateLevel();
    }


}
