using UnityEngine;
using System.Collections;

public interface IBelongsToPlayer  {

    void SetPlayer(PlayerController player);

    PlayerController GetPlayer();
}
