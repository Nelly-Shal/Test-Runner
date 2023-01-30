using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{

    public static KeyManager instance;

    public enum Direction { Left, Right, A, D }

    public delegate void MoveDelegate(bool[] keys);
    public MoveDelegate MoveEvent;
    bool[] key = new bool[4];

    bool KeyA() { return Input.GetKey(KeyCode.A); }
    bool KeyD() { return Input.GetKey(KeyCode.D); }
    bool KeyLeft() { return Input.GetKey(KeyCode.LeftArrow); }
    bool KeyRight() { return Input.GetKey(KeyCode.RightArrow); }


    void Awake()
    {
        instance = this;
    }

    void Update()
    {

        if (KeyA())
        {
            key[(int)Direction.A] =true;
        }
        else if (KeyD())
        {
            key[(int)Direction.D] = true;
        }
        else if (KeyLeft())
        {
            key[(int)Direction.Left] = true;
        }
        else if (KeyRight())
        {
            key[(int)Direction.Right] = true;
        }
        SendKey();
    }

    public void SendKey()
    {
        if (key[0] || key[1] || key[2] || key[3])
        {
            MoveEvent?.Invoke(key);
        }
        Reset();
    }

    private void Reset()
    {
        for (int i = 0; i < 4; i++)
        {
            key[i] = false;
        }
    }
}
