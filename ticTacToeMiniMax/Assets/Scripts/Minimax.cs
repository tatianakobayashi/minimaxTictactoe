using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Minimax : MonoBehaviour
{
    public enum Player { None, X, O }
    public Player[] board;
    private Player currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        board = new Player[9];
        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "Restart"))
        {
            RestartGame();
        }

        for (int i = 0; i < 9; i++)
        {
            int row = i / 3;
            int col = i % 3;
            Rect rect = new Rect(10 + col * 70, 50 + row * 70, 70, 70);

            if (GUI.Button(rect, ""))
            {
                if (board[i] == Player.None)
                {
                    board[i] = currentPlayer;
                    currentPlayer = (currentPlayer == Player.X) ? Player.O : Player.X;
                }
            }

            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.fontSize = 40;
            style.alignment = TextAnchor.MiddleCenter;
            GUI.Button(rect, GetPlayerSymbol(board[i]), style);
        }

        if (CheckGameOver())
        {
            GUI.Label(new Rect(10, 170, 100, 30), "Game Over");
        }
    }

    private void RestartGame()
    {
        currentPlayer = Player.X;
        for (int i = 0; i < 9; i++)
        {
            Debug.Log(i);
            board[i] = Player.None;
        }
    }

    private string GetPlayerSymbol(Player spot)
    {
        switch (spot) {
            case Player.X:
                return "X";
            case Player.O:
                return "O";
            default:
                return " ";
        }
    }

    private bool CheckGameOver()
    {
        Player[,] matriz = {
            { board[0], board[1], board[2] },
            { board[3], board[4], board[5] },
            { board[6], board[7], board[8] }
        };



        Player[] linha1 = { board[0], board[1], board[2] };

        linha1.Skip(1).All(s => s == linha1[0]);


        bool[] matriz2 = {
            new Player[]{ board[1], board[2] }.All(s => s == board[0]),
            new Player[]{ board[4], board[5] }.All(s => s == board[3]),
            new Player[]{ board[7], board[8] }.All(s => s == board[6]),
            new Player[]{ board[3], board[6] }.All(s => s == board[0]),
            new Player[]{ board[4], board[7] }.All(s => s == board[1]),
            new Player[]{ board[5], board[8] }.All(s => s == board[2]),
            new Player[]{ board[4], board[8] }.All(s => s == board[0]),
            new Player[]{ board[4], board[6] }.All(s => s == board[2])
        };

        return;
    }
}
