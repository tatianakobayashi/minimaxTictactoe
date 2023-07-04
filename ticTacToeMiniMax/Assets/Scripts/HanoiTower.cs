using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class HanoiTower : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Resolver(5, 'A', 'B', 'C');
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Resolver(int tamanho, char inicial, char auxiliar, char final)
    {
        if(tamanho == 1)
        {
            Debug.Log($"Mover disco {tamanho} de {inicial} para {final}");
            return;
        }

        Resolver(tamanho - 1, inicial, final, auxiliar);
        Debug.Log($"Mover disco {tamanho} de {inicial} para {final}");
        Resolver(tamanho - 1, auxiliar, inicial, final);
    }
    /*
     (2, a, b, c) => {
        (1, a, c, b) => Mover disco 1 de A para B;
        Mover disco 2 de A para C;
        (1, b, a, c) => Mover disco 1 de B para C;
    }

    
     (3, a, b, c) => {
    
        (2, a, c, b) => {
            (1, a, b, c) => Mover disco 1 de A para C;
            Mover disco 2 de A para B;
            (1, c, a, b) => Mover disco 1 de C para B;
        }
        Mover disco 3 de A para C;
        (2, b, a, c) => {
            (1, b, c, a) => Mover disco 1 de B para A;
            Mover disco 2 de B para C;
            (1, a, b, c) => Mover disco 1 de A para C;
        }
    }

     
     
     */



    /*
     tam: 1
        1 A -> C
     
     tam 2
            1 A -> B    
        2 A -> C
            1 B -> C
     
     tam 3
                1  A -> C
            2 A -> B
                1 C -> B
        3 A -> C
                1 B -> A
            2 B -> C
                1 A -> C
     
     
     */
}
