using System;
using UnityEngine;

// Ein Projektil, das von Türmen geschossen wird und beim Aufprall Schaden an einem Gegner verursacht.
public class BaseProjectile : MonoBehaviour
{
    // Positionsinformationen für die Bewegung zum Ziel
    public Vector3 Turret { get; set; }
    // Transform des Ziels wird für die aktualisierung der Zielposition gespeichert
    public Transform Target { get; set; }
    // Für den Fall, dass keine Zielverfolgung erwünscht ist, wird nur die Position gespeichert
    public Vector3 TargetLocation { set; get; }

    // Den Schaden der dem Gegner zugefügt wird.
    public DamageInfo Damage { set; get; }

    // hat der Turm bereits ein Ziel anvisiert
    public bool IsLockedOnTarget { set; get; }
    // Zeit die das Projektil zum Ziel benötigt
    public float TimeToTarget { set; get; }
    // Wie weit ist die Bewegung zum Ziel abgeschlossen. 0.0f kurz nach Abschuss; 1.0f Ziel erreicht
    private float transition = 0.0f;

    // Flag zeigt an ob das Projektil abgefeuert wurde
    private bool isLaunched = false;

    public bool splash;

    public GameObject explosion;

    // Basis Initialisierung 
    public BaseProjectile()
    {
        IsLockedOnTarget = false;
        TimeToTarget = 5.0f;
    }

    // Ständige Update Routine
    private void Update()
    {
        // Wenn das Projektil noch nicht abgefeuert wurde
        if (!isLaunched)
        {
            // Springe zurück
            return;
        }

        // Fortschritt der Bewegung aktualisieren
        transition += Time.deltaTime / TimeToTarget;

        // Wenn Ziel erreicht.
        if (transition >= 1.0f)
        {
            // Aufprallfunktion
            ReachTarget();
        }

        // Wenn Zielverfolgung aktiviert, aktualisiere die Zielkoordinate
        if (IsLockedOnTarget && Target)
        {
            TargetLocation = Target.position;
        }

        transform.LookAt(TargetLocation);
        transform.Rotate(Vector3.up, 90);

        // Bewegung zum Ziel.
        transform.position = Vector3.Lerp(Turret, TargetLocation, transition);
    }

    // Aufprallfunktion
    // TODO: 
    //      - Verfehlene implementieren: NotLockedOn, Ziel erreicht, aber keine Kollision -> kein Schaden 
    protected virtual void ReachTarget()
    {
        Vector3 posi = new Vector3(transform.position.x, 1.0f, transform.position.z);
        GameObject explosionInstance = Instantiate(explosion, posi, Quaternion.identity) as GameObject;
        Destroy(explosionInstance, 0.1f);
        if (Target)
        {
            Target.SendMessage("OnDamage", Damage);
        }
        Destroy(gameObject);
    }

    // Abschussfunktion
    // Started das Projektil und setzt Ziel und Ursprung fest.
    public virtual void Launch(Transform turret, Transform target, DamageInfo dmg)
    {
        
        isLaunched = true;
        IsLockedOnTarget = true;
        // Ursprung
        Turret = turret.position;
        // Ziel
        Target = target; // Zum Zielverfolgen wird das Transform mitgespeichert
        TargetLocation = target.position; // Die Zielposition.       
        Damage = dmg; 
        TimeToTarget = 0.5f;
    }
}
