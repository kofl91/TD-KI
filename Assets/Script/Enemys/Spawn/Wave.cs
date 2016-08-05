using UnityEngine;

//  Eine Wellen-Struktur zum Spawnen von Gegnern
public class Wave : MonoBehaviour
{
    // Die HP die alle Einheiten dieser Welle haben sollen
    public int enemyHP;

    // Die Belohnung die man nach dem Töten dieser Einheit erhält
    public int enemyBounty;

    // Die ID des Prefabs aus dem diese Einheit gebaut wird
    public int enemyID;

    // Die Anzahl an Einheiten die geschickt werden soll
    public int maxCount;

    // Die Anzahl der Einheiten die noch zu schicken sind
    private int count;

    // Der Zeitabstand in dem die Einheiten geschickt werden
    public float interval;


    // Setzt eine Welle zurück
    internal void Reset()
    {
        count = maxCount;
    }

    // Verringert die Anzahl der Gegner um einen
    internal void Decr()
    {
        count--;
    }

    // Gibt an ob alle Einheiten dder Welle gespawnt wurden
    internal bool hasEnded()
    {
        return count <= 0;
    }
}