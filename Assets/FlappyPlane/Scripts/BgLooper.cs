using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyPlaneSession
{
    public class BgLooper : MonoBehaviour
    {
        public int numBgCount = 5;

        public int obstacleCount = 0;
        public Vector3 obstacleLastPosition = Vector3.zero;

        void Start()
        {
            Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
            obstacleLastPosition = obstacles[0].transform.position;
            obstacleCount = obstacles.Length;

            for (int i = 0; i < obstacleCount; i++)
            {
                obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
            }
        }
        // 트리거 충돌: 매개변수가 Collision이 아닌 Collider, 충돌에 대한 정보가 아닌, 충돌체에 대한 정보
        public void OnTriggerEnter2D(Collider2D collision)
        {
            //Debug.Log("Triggered: " + collision.name);

            if (collision.CompareTag("BackGround"))
            {
                float widthOfBgObject = ((BoxCollider2D)collision).size.x;
                Vector3 pos = collision.transform.position;

                pos.x += widthOfBgObject * numBgCount;
                collision.transform.position = pos;
                return; // 충돌시 백그라운드라면 장애물이 아니란 뜻으로, 아래 코드x
            }

            Obstacle obstacle = collision.GetComponent<Obstacle>();
            if (obstacle)
            {
                obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
            }
        }
    }
}

