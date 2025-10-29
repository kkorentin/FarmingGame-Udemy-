using UnityEngine;

/// <summary>
/// Contr�le simple d'une cam�ra orthographique qui suit un joueur
/// et reste limit�e � l'int�rieur de deux points (clampMin / clampMax).
/// </summary>
public class CameraController : MonoBehaviour
{
    // R�f�rence vers le Transform de la cible (ici le joueur) que la cam�ra doit suivre.
    private Transform target;

    // Deux Transforms plac�s dans la sc�ne pour d�finir les limites (min et max) de la cam�ra.
    public Transform clampMin, clampMax;

    private Camera cam;

    private float halfHeight, halfWidth;

    void Start()
    {
        // Recherche une instance de PlayerController dans la sc�ne et r�cup�re son Transform.
        // Remarque : FindAnyObjectByType peut renvoyer null si aucun PlayerController n'existe.
        //target = FindAnyObjectByType<PlayerController>().transform;

        target = PlayerController.instance.transform;
        
        // D�tache les objets clampMin et clampMax de leurs parents dans la hi�rarchie.
        // Cela les place au niveau racine de la sc�ne et conserve leurs positions.
        clampMin.SetParent(null);
        clampMax.SetParent(null);

        cam = GetComponent<Camera>();

        // Pour une cam�ra orthographique, orthographicSize correspond � la demi-hauteur en unit�s.
        // Ici on stocke cette demi-hauteur dans halfHeight.
        // on utilise orthographic pour la cam�ra 2D
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;
    }

    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        // Copie de la position actuelle avant d'appliquer les limites.
        Vector3 clampedPosition = transform.position;

        // Clamp (restreint) la position x entre clampMin.position.x et clampMax.position.x.
        // Mathf.Clamp(valeur, min, max) renvoie la valeur limit�e entre min et max.
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, clampMin.position.x +halfWidth, clampMax.position.x - halfWidth);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, clampMin.position.y + halfHeight, clampMax.position.y - halfHeight);

        transform.position = clampedPosition;
    }
}