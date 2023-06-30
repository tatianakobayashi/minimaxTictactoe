using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static Minimax;
using Random = System.Random;

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
            MakeAIPlay4();
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
        return GetWinner() != Player.None || board.All( b => b != Player.None);
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
            if (board[i] == Player.None)
            {
                board[i] = Player.O;

                int m = MiniMax(Player.X);
                if (m == 1)
                {
                    position = i;
                    Debug.Log(i);
                }
                board[i] = Player.None;
            }
        }
        Debug.Log("p: " + position);
        board[position] = Player.O;
    }
    void MakeAIPlay4()
    {
        int position = 0;
        List<int> possibilities = new List<int>();
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == Player.None)
            {
                board[i] = Player.O;
                if(GetWinner() == Player.O)
                {
                    //Debug.Log("posicao: " + j + " depth: " + depth);
                    // position = i;
                    Debug.Log("break");
                    return;
                    // Debug.Log("i: " + i + "j: " +  j);
                }

                int m = MiniMax(Player.X);
                if (m == 1)
                {
                    possibilities.Add(i);
                    //position = i;
                    Debug.Log(i);
                }
                board[i] = Player.None;
            }
        }
        Debug.Log("p: " + position);

        Random r = new Random();
        position = r.Next(possibilities.Count);

        board[possibilities[position]] = Player.O;
    }

    int RandomPosition()
    {
        Random random = new Random();
        while(true)
        {
            int i = random.Next(0, board.Length);
            if (board[i] == Player.None) return i;
        }
    }

    void MakeAIPlay2()
    {
        string tree = "";
        int position = -1;
        int depth = int.MaxValue;
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == Player.None)
            {
                board[i] = Player.O;

                tree += "O in " + i + "\nX: ";


                if (GetWinner() == Player.O)
                {
                    //Debug.Log("posicao: " + j + " depth: " + depth);
                    position = i;
                    Debug.Log("break");
                    break;
                    // Debug.Log("i: " + i + "j: " +  j);
                }


                for (int j = 0; j < 9; j++) {
                    if (board[j] == Player.None)
                    {
                        board[j] = Player.X;

                        (int, int) m = MiniMax(Player.O, 0);
                        Debug.Log("m: " + m);
                        if (m.Item1 == 1 && m.Item2 < depth)
                        {
                            Debug.Log("posicao: " + j + " depth: " + depth);
                            position = j;
                            depth = m.Item1;
                            // Debug.Log("i: " + i + "j: " +  j);
                        }

                        if (CheckGameOver())
                        {
                            Debug.Log("i: " + i + " j: " + j + " winner: " + GetPlayerSymbol(GetWinner()) + " m: " + m);
                        }
                        tree += j + " = (" + m + ") | ";
                        board[j] = Player.None;
                    }


                }

                tree += "\n";

                board[i] = Player.None;
            }
        }

        if (position < 0) position = RandomPosition();
        Debug.Log("TREE: " + tree);
        board[position] = Player.O;
    }

    void MakeAIPlay3()
    {
        int position = -1;
        int depth = int.MaxValue;
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == Player.None)
            {
                board[i] = Player.O;


                if (GetWinner() == Player.O)
                {
                    //Debug.Log("posicao: " + j + " depth: " + depth);
                    position = i;
                    Debug.Log("break");
                    break;
                    // Debug.Log("i: " + i + "j: " +  j);
                }

                (int, int) m = MiniMax(Player.X, 0);

                if (m.Item1 == 1 && m.Item2 < depth)
                {
                    Debug.Log("posicao: " + i + " depth: " + depth);
                    position = i;
                    depth = m.Item1;
                    // Debug.Log("i: " + i + "j: " +  j);
                }

                board[i] = Player.None;
            }
        }

        if (position < 0) position = RandomPosition();

        board[position] = Player.O;
    }

    int MiniMax(Player player)
    {
        if (CheckGameOver())
        {
            //string[] a = board.Select(GetPlayerSymbol).ToArray();

            //Debug.Log("Game Over: " + string.Join("|", a));

            return scores[((int)player)];
        }

        int bestScore = (player == Player.O) ? int.MinValue : int.MaxValue;

        string v = "PLAYER " + GetPlayerSymbol(player) + "\n";
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == Player.None)
            {
                board[i] = player;

                //Debug.Log(bestScore);
                v += "POS: " + i + " score: (" + bestScore;

                if (player == Player.O)
                    bestScore = Mathf.Max(bestScore, MiniMax(Player.X));
                else
                    bestScore = Mathf.Min(bestScore, MiniMax(Player.O));

                v += "|" + bestScore + ")\n";

                //Debug.Log(bestScore);

                board[i] = Player.None;
            }
        }
        Debug.Log(v);

        return bestScore;
    }

    (int, int) MiniMax(Player player, int depth)
    {
        if (CheckGameOver())
        {
            //string[] a = board.Select(GetPlayerSymbol).ToArray();

            //Debug.Log("Game Over: " + string.Join("|", a));

            return (scores[(int)player], depth);
        }

        int bestScore = (player == Player.O) ? int.MinValue : int.MaxValue;

        int maxDepth = int.MaxValue;

        string v = "PLAYER " + GetPlayerSymbol(player) + "\n";
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == Player.None)
            {
                board[i] = player;

                //Debug.Log(bestScore);
                v += "POS: " + i + " score: (" + bestScore;

                if (player == Player.O)
                {
                    (int, int) m = MiniMax(Player.X, depth + 1);
                    if(m.Item1 >= bestScore)
                        maxDepth = Mathf.Min(maxDepth, m.Item2);
                    bestScore = Mathf.Max(bestScore, m.Item1);
                }
                else
                {
                    (int, int) m = MiniMax(Player.O, depth + 1);
                    if (m.Item1 <= bestScore)
                        maxDepth = Mathf.Min(maxDepth, m.Item2);
                    bestScore = Mathf.Min(bestScore, m.Item1);
                }

                v += "|" + bestScore + ")\n";

                //Debug.Log(bestScore);

                board[i] = Player.None;
            }
        }
        //Debug.Log(v);

        return (bestScore, maxDepth);
    }
}
