using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { white, green, black, red, blue };

public class Card : MonoBehaviour
{
    public int score;
    public CardType type;
    public int[] prices;

}
