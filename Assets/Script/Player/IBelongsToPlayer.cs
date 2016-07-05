using UnityEngine;
using System.Collections;

public interface IBelongsToPlayer  {

    void SetPlayer(int player);

    PlayerController GetPlayer();
}
