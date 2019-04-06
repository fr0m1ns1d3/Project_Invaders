using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameValues : MonoBehaviour
{

public enum Difficulties { Easy, Hard};

    public static bool IsMultiplayer;
    public static Difficulties Difficulty = Difficulties.Easy;
}

