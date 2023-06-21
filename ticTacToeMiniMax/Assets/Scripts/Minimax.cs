using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        if (currentPlayer == Player.O)
        {

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


        return GetWinner() == Player.O;
    }

    Player GetWinner()
    {
        Player[] array1 = {
            // Linhas
            new Player[]{ board[1], board[2] }.All(s => s == board[0]) ? board[0] : Player.None,
            new Player[]{ board[4], board[5] }.All(s => s == board[3]) ? board[3] : Player.None,
            new Player[]{ board[7], board[8] }.All(s => s == board[6]) ? board[6] : Player.None,
            // Colunas
            new Player[]{ board[3], board[6] }.All(s => s == board[0]) ? board[0] : Player.None,
            new Player[]{ board[4], board[7] }.All(s => s == board[1]) ? board[1] : Player.None,
            new Player[]{ board[5], board[8] }.All(s => s == board[2]) ? board[2] : Player.None,
            // Diagonais
            new Player[]{ board[4], board[8] }.All(s => s == board[0]) ? board[0] : Player.None,
            new Player[] { board[4], board[6] }.All(s => s == board[2]) ? board[2] : Player.None
        };


        return array1.All(p => p == Player.None) ? Player.None : Array.Find(array1, p => p != Player.None);

    }

    /*
     ROTINA minimax(nó, profundidade, maximizador)
        SE nó é um nó terminal OU profundidade = 0 ENTÃO
            RETORNE o valor da heurística do nó
        SENÃO SE maximizador é FALSE ENTÃO
            ? ? +?
            PARA CADA filho DE nó
                ? ? min(?, minimax(filho, profundidade-1,true))
            FIM PARA
            RETORNE ?
        SENÃO
            //Maximizador
            ? ? -?
            //Escolher a maior dentre as perdas causadas pelo minimizador
            PARA CADA filho DE nó
                ? ? max(?, minimax(filho, profundidade-1,false))
            FIM PARA
            RETORNE ?
        FIM SE
    FIM ROTINA
     */

    int MiniMax(Player player)
    {
        return 0;
    }
}
