using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Player
    {
        X, O
    }

    private Player currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = Player.X;

        int f = 8;
        int fib = Fibonnacci(f);
        Debug.Log(fib);
    }

    // Update is called once per frame
    void Update()
    {
        // Ternario();
        // IfElse();

    }

    void IfElse()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //if (currentPlayer == Player.X) currentPlayer = Player.O; else currentPlayer = Player.X;

            Debug.Log($"O jogador atual é o {currentPlayer}");
        }
    }

    void Ternario()
    {
        currentPlayer = Input.GetKeyDown(KeyCode.A) ? (currentPlayer == Player.X) ? Player.O : Player.X : currentPlayer;
    }

    int Fibonnacci(int v)
    {
        if (v == 0 || v == 1)
        {
            //Debug.Log(v);
            return v;
        }

        int final = Fibonnacci(v - 1) + Fibonnacci(v - 2);

        // Debug.Log(final);

        return final;
    }
}