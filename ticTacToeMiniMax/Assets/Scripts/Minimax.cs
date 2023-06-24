using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static Minimax;

public class Minimax : MonoBehaviour
{
    public enum Player { None, X, O }
    public Player[] board;
    [SerializeField] private Player currentPlayer;
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
            currentPlayer = Player.X;
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
        return GetWinner() != Player.None || board.All(b=> b != Player.None);
    }

    Player GetWinner()
    {
        int[,] lines1 =
        {
            // Linhas
            { 0, 1, 2 },
            { 3, 4, 5 },
            { 6, 7, 8 },
            // Colunas
            { 0, 3, 6 },
            { 1, 4, 7 },
            { 2, 5, 8 },
            // Diagonais
            { 0, 4, 8 },
            { 2, 4, 6 }
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
    }

    Player GetLineResult(int reference, int position1, int position2)
    {
        return (board[position1] == board[reference] && board[position2] == board[reference] && board[reference] != Player.None) ? board[reference] : Player.None;
    }

    void MakeAIPlay()
    {
        int position = 0;
        for (int i = 0; i < 9; i++)
        {
            //Debug.Log("Make AI Play " + i);
            if (board[i] == Player.None)
            {
                board[i] = Player.O;
                if (MiniMax(Player.X) == 1)
                    return;
                else
                    board[i] = Player.None;
            }
        }

    }

    int MiniMax(Player player)
    {
        //Debug.Log("Player " + GetPlayerSymbol(player));

        if (CheckGameOver())
            return scores[((int)player)];

        int bestScore = (player == Player.O) ? int.MinValue : int.MaxValue;
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == Player.None)
            {
                //Debug.Log("board " + i);

                board[i] = player;

                if (player == Player.O)
                    bestScore = Mathf.Max(bestScore, MiniMax(Player.X));
                else
                    bestScore = Mathf.Min(bestScore, MiniMax(Player.O));

                board[i] = Player.None;
            }
        }
        //Debug.Log("best score " + bestScore);

        return bestScore;
    }
}
