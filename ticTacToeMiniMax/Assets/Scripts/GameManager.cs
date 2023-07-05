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
        /*
        currentPlayer = Player.X;

        int f = 8;
        int fib = Fibonnacci(f);
        Debug.Log(fib);
        */

        ContagemRegressiva(10);

        Debug.Log("Soma dos digitos: " + SomaDosDigitos(4123));

        Debug.Log("Reverso: " + Reverso("ABCDE"));

        Debug.Log("Palindromo: " + Palindromo("ANNAB"));
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

    void ContagemRegressiva(int contagem)
    {
        if (contagem == 0) Debug.Log("Contagem regressiva finalizada");
        else
        {
            Debug.Log(contagem);
            ContagemRegressiva(contagem - 1);
        }
    }

    int SomaDosDigitos(int numero)
    {
        if (numero > 0)
        {
            return numero % 10 + SomaDosDigitos(numero / 10);
        }
        return 0;
    }

    string Reverso(string palavra)
    {
        if (palavra.Length == 0) return "";
        int index = palavra.Length - 1;
        return palavra.Substring(index) + Reverso(palavra.Remove(index));
    }

    bool Palindromo(string palavra)
    {
        if (palavra.Length < 2)
            return true;

        return palavra.Substring(0, 1).ToLower() == palavra.Substring(palavra.Length - 1).ToLower() && Palindromo(palavra.Substring(1, palavra.Length - 2));
    }
}