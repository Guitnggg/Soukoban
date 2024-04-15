using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    //配列の宣言
    int[] map;

    //メソッド化
    private void printArray()
    {
        string debugText = "";

        for (int i = 0; i < map.Length; i++)
        {
            debugText += map[i].ToString() + ",";
        }
        Debug.Log(debugText);
    }

    //クラスの中、メソッドの外に書くことに注意
    //返り値の型に注意
    int GetPlayerIndex()
    {
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] == 1)
            {
                return i;
            }
        }
        return -1;
    }

    bool MoveNumber(int number, int moveFrom, int moveTo)
    {
        //移動先が範囲外なら移動不可
        if (moveTo < 0 || moveTo >= map.Length) 
        { 
            return false;
        }

        //移動先に２（箱）があったら
        if (map[moveTo]==2)
        {
            //どの方向へ移動するかを算出
            int velocity = moveTo - moveFrom;
            //プレイヤーの移動先から、さらに先へ２（箱）を移動させる
            //箱の移動処理、MoveNumberメソッド内でMoveNumberメソッド呼び、処理が再帰している。
            //移動可不可をboolで記録
            bool success = MoveNumber(2, moveTo, moveTo + velocity);

            //もし箱が移動しっぱしたらプレイヤーの移動も失敗
            if (!success) 
            { 
                return false;
            }
        }

        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;
    }



    // Start is called before the first frame update
    void Start()
    {
        //配列の実態の作成と初期化
        map = new int[] { 2, 0, 0, 1, 0, 2, 2, 0, 0 };

        printArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //メソッド化した処理を使用
            int playerIndex = GetPlayerIndex();

            MoveNumber(1, playerIndex, playerIndex + 1);
            printArray();

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int playerIndex = GetPlayerIndex();

            MoveNumber(1, playerIndex, playerIndex - 1);
            printArray();
        }
    }
}

