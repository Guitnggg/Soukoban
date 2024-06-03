using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject boxPrefab;
    public GameObject goalPrefab;
    public GameObject particlePrefab;
    
    public GameObject clearText;

    private int[,] map;
    private GameObject[,] field;

    // �Q�[���̏�����
    void Start()
    {
        InitializeGame();
    }

    // �Q�[���̏���������
    void InitializeGame()
    {
        Screen.SetResolution(1280, 720, false);

        string debugText = "";

        map = new int[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 2, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 1, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 3},
        };

        field = new GameObject[map.GetLength(0), map.GetLength(1)];

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                debugText += map[y, x].ToString() + ",";
                if (map[y, x] == 1)
                {
                    field[y, x] = Instantiate(playerPrefab, new Vector3(x, map.GetLength(0) - 1 - y, 0.0f), Quaternion.identity);
                }
                if (map[y, x] == 2)
                {
                    field[y, x] = Instantiate(boxPrefab, new Vector3(x, map.GetLength(0) - 1 - y, 0.0f), Quaternion.identity);
                }
                if (map[y, x] == 3)
                {
                    field[y, x] = Instantiate(goalPrefab, new Vector3(x, map.GetLength(0) - 1 - y, 0.0f), Quaternion.identity);
                }
            }
            debugText += "\n";
        }
        Debug.Log(debugText);
    }

    // �t���[�����Ƃ̍X�V
    void Update()
    {
        HandleInput();

        // �����N���A���Ă�����
        if (IsCleared())
        {
            clearText.SetActive(true);
        }
    }

    // ���͏���
    void HandleInput()
    {
        // �E�ړ�
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(1, 0));
        }

        // ���ړ�
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(-1, 0));
        }

        // ��ړ�
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(0, -1));
        }

        // ���ړ�
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(0, 1));
        }

        // R���������烊�Z�b�g
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }

        // �����N���A���Ă�����
        if(IsCleared()) { Debug.Log("Clear!"); }
    }

    // �Q�[�����Z�b�g
    void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // �v���C���[�̈ʒu���擾
    private Vector2Int GetPlayerIndex()
    {
        for (int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
            {
                if (field[y, x] != null && field[y, x].tag == "Player")
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }

    // �ړ�����
    bool MoveNumber(Vector2Int moveFrom, Vector2Int moveTo)
    {
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0)) { return false; }
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1)) { return false; }

        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            Vector2Int velocity = moveTo - moveFrom;
            bool success = MoveNumber(moveTo, moveTo + velocity);
            if (!success) { return false; }
        }

        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        Vector3 moveToPosition = new Vector3(moveTo.x, map.GetLength(0) - 1 - moveTo.y, 0);
        field[moveFrom.y, moveFrom.x] = null;
        field[moveTo.y, moveTo.x].GetComponent<Move>().Moveto(moveToPosition);
        return true;
    }

    // �N���A����
    bool IsCleared()
    {
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 3 && (field[y, x] == null || field[y, x].tag != "Box"))
                {
                    return false;
                }
            }
        }
        return true;
    }
}
