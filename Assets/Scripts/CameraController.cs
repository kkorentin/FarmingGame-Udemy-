using UnityEngine;

/// <summary>
/// Contrôle simple d'une caméra orthographique qui suit un joueur
/// et reste limitée à l'intérieur de deux points (clampMin / clampMax).
/// </summary>
public class CameraController : MonoBehaviour
{
    // Référence vers le Transform de la cible (ici le joueur) que la caméra doit suivre.
    private Transform target;

    // Deux Transforms placés dans la scène pour définir les limites (min et max) de la caméra.
    public Transform clampMin, clampMax;

    private Camera cam;

    private float halfHeight, halfWidth;

    void Start()
    {
        // Recherche une instance de PlayerController dans la scène et récupère son Transform.
        // Remarque : FindAnyObjectByType peut renvoyer null si aucun PlayerController n'existe.
        //target = FindAnyObjectByType<PlayerController>().transform;

        target = PlayerController.instance.transform;
        
        // Détache les objets clampMin et clampMax de leurs parents dans la hiérarchie.
        // Cela les place au niveau racine de la scène et conserve leurs positions.
        clampMin.SetParent(null);
        clampMax.SetParent(null);

        cam = GetComponent<Camera>();

        // Pour une caméra orthographique, orthographicSize correspond à la demi-hauteur en unités.
        // Ici on stocke cette demi-hauteur dans halfHeight.
        // on utilise orthographic pour la caméra 2D
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;
    }

    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        // Copie de la position actuelle avant d'appliquer les limites.
        Vector3 clampedPosition = transform.position;

        // Clamp (restreint) la position x entre clampMin.position.x et clampMax.position.x.
        // Mathf.Clamp(valeur, min, max) renvoie la valeur limitée entre min et max.
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, clampMin.position.x +halfWidth, clampMax.position.x - halfWidth);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, clampMin.position.y + halfHeight, clampMax.position.y - halfHeight);

        transform.position = clampedPosition;
    }
}