using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheStackSession
{
    public class TheStack : MonoBehaviour
    {
        // Const Value
        private const float BoundSize = 3.5f;
        private const float MovingBoundsSize = 3f;
        private const float StackMovingSpeed = 5.0f;
        private const float BlockMovingSpeed = 3.5f;
        private const float ErrorMargin = 0.1f;

        public GameObject originBlock = null;

        private Vector3 prevBlockPosition;
        private Vector3 desiredPosition;
        private Vector3 stackBounds = new Vector2(BoundSize, BoundSize);

        Transform lastBlock = null;
        float blockTransition = 0f;
        float secondaryPosition = 0f;

        int stackCount = -1;
        public int Score { get { return stackCount; } }
        int comboCount = 0;
        public int Combo { get { return comboCount; } }

        private int maxCombo = 0;
        public int MaxCombo { get => maxCombo; } // 람다로도 리턴 가능

        public Color prevColor;
        public Color nextColor;

        bool isMovingX = true;

        int bestScore = 0;
        public int BestScore { get => bestScore; }

        int bestCombo = 0;
        public int BestCombo { get => bestCombo; }

        private const string BestScoreKey = "BestScore";
        private const string BestComboKey = "BestCombo";

        private bool isGameOver = true;
        
        void Start()
        {
            StartCoroutine(FadeManager.Instance.FadeIn());
            if (originBlock == null)
            {
                Debug.Log("OriginBlock is NULL");
                return;
            }

            bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
            bestCombo = PlayerPrefs.GetInt(BestComboKey, 0);

            prevColor = GetRandomColor();
            nextColor = GetRandomColor();

            prevBlockPosition = Vector3.down;
            // 맨 처음 블록 하나 소환
            Spawn_Block();
            Spawn_Block();
        }

        void Update()
        {
            if (isGameOver) return;

            if (Input.GetMouseButtonDown(0))
            {
                if (PlaceBlock())
                {
                    Spawn_Block();
                }
                else
                {
                    // 게임 오버
                    Debug.Log("GameOver");
                    UpdateScore();
                    isGameOver = true;
                    GameOverEffect();
                    UIManager.Instance.SetScoreUI();
                }
            }
            MoveBlock();

            transform.position = Vector3.Lerp(transform.position, desiredPosition, StackMovingSpeed * Time.deltaTime);
        }

        bool Spawn_Block()
        {
            // 이전블럭 저장
            if (lastBlock != null)
                prevBlockPosition = lastBlock.localPosition;

            GameObject newBlock = null;
            Transform newTrans = null;

            newBlock = Instantiate(originBlock);

            if (newBlock == null)
            {
                Debug.Log("NewBlock Instantiate Failed!");
                return false;
            }
            ColorChange(newBlock);

            // 참조 넘겨주기(작업 편의성을 위해)
            newTrans = newBlock.transform;
            newTrans.parent = transform;
            newTrans.localPosition = prevBlockPosition + Vector3.up;
            newTrans.localRotation = Quaternion.identity;
            newTrans.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

            stackCount++;

            desiredPosition = Vector3.down * stackCount;
            blockTransition = 0f;

            lastBlock = newTrans;
            // x, z축으로 번갈아가면서 이동하게 하기
            isMovingX = !isMovingX;

            UIManager.Instance.UpdateScore();
            return true;
        }
        Color GetRandomColor()
        {
            float r = Random.Range(100f, 250f) / 255f;
            float g = Random.Range(100f, 250f) / 255f;
            float b = Random.Range(100f, 250f) / 255f;

            return new Color(r, g, b);
        }

        void ColorChange(GameObject go)
        {
            Color applyColor = Color.Lerp(prevColor, nextColor, stackCount % 11 / 10f);

            Renderer rn = go.GetComponent<Renderer>();

            if (rn == null)
            {
                Debug.Log("Renderer is NULL!");
                return;
            }

            rn.material.color = applyColor;
            // 블록과 배경의 색 구분을 위해, 배경은 좀 어둡게
            Camera.main.backgroundColor = applyColor - new Color(0.1f, 0.1f, 0.1f);

            if (applyColor.Equals(nextColor) == true)
            {
                prevColor = nextColor;
                nextColor = GetRandomColor();
            }
        }
        void MoveBlock()
        {
            blockTransition += Time.deltaTime * BlockMovingSpeed;

            float movePosition = Mathf.PingPong(blockTransition, BoundSize) - BoundSize / 2;

            if (isMovingX)
            {
                // 강의 영상 해설 잘못된거 같음
                // MovingBoundsSize: 얼만큼 이동이 가능한가 
                // 기존 소환된 블록이 이동할 수 있는 범위
                // -> BoundSize = 블록 사이즈니까 범위는 블록 사이즈 만큼
                // MovingBoundsSize = 3일 시
                // 
                lastBlock.localPosition = new Vector3(movePosition * MovingBoundsSize, stackCount, secondaryPosition);
            }
            else
            {
                lastBlock.localPosition = new Vector3(secondaryPosition, stackCount, -movePosition * MovingBoundsSize);
            }
        }
        bool PlaceBlock()
        {
            Vector3 lastPosition = lastBlock.localPosition;

            if (isMovingX)
            {
                float deltaX = prevBlockPosition.x - lastPosition.x;
                bool isNegativeNum = deltaX < 0 ? true : false;

                deltaX = Mathf.Abs(deltaX);
                if (deltaX > ErrorMargin)
                {
                    stackBounds.x -= deltaX;
                    if (stackBounds.x <= 0)
                    {
                        return false; // 못 올려 놓는 경우
                    }
                    // 놓는 블록을 배치하고 튀어나온 부분을 자르기
                    // 자른 후의 중심점
                    float middle = (prevBlockPosition.x + lastPosition.x) / 2;
                    // 튀어나온 부분 자르는 부분에 해당
                    lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

                    Vector3 tempPosition = lastBlock.localPosition;
                    tempPosition.x = middle;
                    // lastPosition은 어차피 버릴건데 왜...?
                    // 러그 생성에 쓰임
                    lastBlock.localPosition = lastPosition = tempPosition;

                    // 러그 생성
                    float rubbleHalfScale = deltaX / 2f;
                    // 러그의 중심 구해서 넣기, 러그의 사이즈는 deltaX
                    CreateRubble(
                        new Vector3(isNegativeNum
                                ? lastPosition.x + stackBounds.x / 2 + rubbleHalfScale
                                : lastPosition.x - stackBounds.x / 2 - rubbleHalfScale
                            , lastPosition.y
                            , lastPosition.z),
                        new Vector3(deltaX, 1, stackBounds.y)
                    );

                    comboCount = 0;
                }
                else // 0.1차이까지는 그냥 위에다 놓기
                {
                    ComboCheck();
                    lastBlock.localPosition = prevBlockPosition + Vector3.up;
                }
            }
            else
            {
                float deltaZ = prevBlockPosition.z - lastPosition.z;
                bool isNegativeNum = deltaZ < 0 ? true : false;

                deltaZ = Mathf.Abs(deltaZ);
                if (deltaZ > ErrorMargin)
                {
                    stackBounds.y -= deltaZ;
                    if (stackBounds.y <= 0)
                    {
                        return false;
                    }

                    float middle = (prevBlockPosition.z + lastPosition.z) / 2;
                    lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

                    Vector3 tempPosition = lastBlock.localPosition;
                    tempPosition.z = middle;
                    lastBlock.localPosition = lastPosition = tempPosition;

                    float rubbleHalfScale = deltaZ / 2f;
                    CreateRubble(
                        new Vector3(
                            lastPosition.x
                            , lastPosition.y
                            , isNegativeNum
                                ? lastPosition.z + stackBounds.y / 2 + rubbleHalfScale
                                : lastPosition.z - stackBounds.y / 2 - rubbleHalfScale),
                        new Vector3(stackBounds.x, 1, deltaZ)
                    );
                    comboCount = 0;

                }
                else
                {
                    ComboCheck();
                    lastBlock.localPosition = prevBlockPosition + Vector3.up;
                }
            }
            //이전 블록의 중심이 바뀌었기 때문에, 이 값 기준으로 블록이 움직이도록 세팅
            secondaryPosition = isMovingX ? lastBlock.localPosition.x : lastBlock.localPosition.z;

            return true;
        }
        void CreateRubble(Vector3 pos, Vector3 scale)
        {
            GameObject go = Instantiate(lastBlock.gameObject);
            go.transform.parent = transform;

            go.transform.localPosition = pos;
            go.transform.localScale = scale;
            go.transform.localRotation = Quaternion.identity;

            go.AddComponent<Rigidbody>();
            go.name = "Rubble";
        }
        void ComboCheck()
        {
            comboCount++;

            if (comboCount > maxCombo)
                maxCombo = comboCount;

            if (comboCount % 5 == 0)
            {
                Debug.Log("5Combo Success!");
                stackBounds += new Vector3(0.5f, 0.5f);
                stackBounds.x =
                    stackBounds.x > BoundSize ? BoundSize : stackBounds.x;
                stackBounds.y =
                    stackBounds.y > BoundSize ? BoundSize : stackBounds.y;
            }
        }
        void UpdateScore()
        {
            if (bestScore < stackCount)
            {
                Debug.Log("최고 점수 갱신");
                bestScore = stackCount;
                bestCombo = maxCombo;

                PlayerPrefs.SetInt(BestScoreKey, bestScore);
                PlayerPrefs.SetInt(BestComboKey, bestCombo);
            }
        }
        void GameOverEffect()
        {
            int childCount = transform.childCount;

            for (int i = 1; i < 20; i++)
            {
                if (childCount < i)
                    break;

                GameObject go =
                    transform.GetChild(childCount - i).gameObject;

                if (go.name.Equals("Rubble"))
                    continue;

                Rigidbody rigid = go.AddComponent<Rigidbody>();

                rigid.AddForce(
                    (Vector3.up * Random.Range(0, 10f)
                     + Vector3.right * (Random.Range(0, 10f) - 5f))
                    * 100f
                );
            }
        }
        public void Restart()
        {
            int childCount = transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

            isGameOver = false;

            lastBlock = null;
            desiredPosition = Vector3.zero;
            stackBounds = new Vector3(BoundSize, BoundSize);

            stackCount = -1;
            isMovingX = true;
            blockTransition = 0f;
            secondaryPosition = 0f;

            comboCount = 0;
            maxCombo = 0;

            prevBlockPosition = Vector3.down;

            prevColor = GetRandomColor();
            nextColor = GetRandomColor();

            Spawn_Block();
            Spawn_Block();
        }

    }
}