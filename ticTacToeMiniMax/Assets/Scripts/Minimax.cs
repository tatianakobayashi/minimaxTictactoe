using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Minimax : MonoBehaviour
{
    public enum Player { None, X, O }
    public Player[] board;
    private Player currentPlayer;
    private int[] scores = { 0, -1, 1 };

    // Start is called before the first frame update
    void Start()
    {
        board = new Player[9];
        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlayer == Player.O && !CheckGameOver())
        {
            // TODO ai move
            MakeAIPlay();
        }
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
            board[i] = Player.None;
        }
    }

    private string GetPlayerSymbol(Player spot)
    {
        switch (spot)
        {
            case Player.X:
                return "X";
            case Player.O:
                return "O";
            default:
                return "";
        }
    }

    private bool CheckGameOver()
    {
        /*
        bool[] array = {
            // Linhas
            new Player[]{ board[1], board[2] }.All(s => s == board[0] && s != Player.None),
            new Player[]{ board[4], board[5] }.All(s => s == board[3] && s != Player.None),
            new Player[]{ board[7], board[8] }.All(s => s == board[6] && s != Player.None),
            // Colunas
            new Player[]{ board[3], board[6] }.All(s => s == board[0] && s != Player.None),
            new Player[]{ board[4], board[7] }.All(s => s == board[1] && s != Player.None),
            new Player[]{ board[5], board[8] }.All(s => s == board[2] && s != Player.None),
            // Diagonais
            new Player[]{ board[4], board[8] }.All(s => s == board[0] && s != Player.None),
            new Player[]{ board[4], board[6] }.All(s => s == board[2] && s != Player.None)
        };
        */

        //return Array.Find(array, p => p);


        return GetWinner() == Player.O || board.All(p => p != Player.None) ;
    }

    Player GetWinner()
    {
        int[,] lines1 =
        {
            {0, 1, 2},
            {3, 4, 5},
            {6, 7, 8 },
            {0, 3, 6 },
            {1, 4, 7 },
            { 2, 5, 8 },
            {0, 4, 8 },
            { 2, 4, 6}
        };

        for(int i = 0; i < 8; i++)
        {
            Player result = GetLineResult(lines1[i, 0], lines1[i, 1], lines1[i, 2]);
            if (result != Player.None)
            {
                return result;
            }
        }
        return Player.None;

        /*
        Player[] lines = {
            // Linhas
            GetLineResult(0, 1, 2),
            GetLineResult(3, 4, 5),
            GetLineResult(6, 7, 8),
            // Colunas
            GetLineResult(0, 3, 6),
            GetLineResult(1, 4, 7),
            GetLineResult(2, 5, 8),
            // Diagonais
            GetLineResult(0, 4, 8),
            GetLineResult(2, 4, 6)
        };


        return lines.All(p => p == Player.None) ? Player.None : Array.Find(lines, p => p != Player.None);
        */
    }

    Player GetLineResult(int reference, int position1, int position2)
    {
        return (board[position1] == board[reference] && board[position2] == board[reference] && board[reference] != Player.None) ? board[reference] : Player.None;
    }

    void MakeAIPlay()
    {
        int position = -1;
        for (int i = 0; i < 9; i++)
        {
            if (board[i] != Player.None && MiniMax(currentPlayer) > 0)
            {
                position = i;
            }
        }

        if (position > -1) board[position] = Player.O;
    }

    int MiniMax(Player player)
    {
        if (CheckGameOver())
            return scores[((int)player)];

        int bestScore = (player == Player.O) ? int.MinValue : int.MaxValue;
        for (int i = 0; i < 9; i++)
        {
            if (board[i] != Player.None)
            {
                board[i] = player;
                int minimax = MiniMax((player == Player.O) ? Player.X : Player.O);

                if (player == Player.O)
                    bestScore = Mathf.Max(bestScore, minimax);
                else
                    bestScore = Mathf.Min(bestScore, minimax);

                board[i] = Player.None;
            }
        }

        return bestScore;
    }
}
