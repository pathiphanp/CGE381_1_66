using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public Player player;

    public void PlayerDie()
    {
        player.DieMode();
    }
}
