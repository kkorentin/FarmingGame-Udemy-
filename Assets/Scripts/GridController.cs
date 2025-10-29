using UnityEngine;

public class GridController : MonoBehaviour
{
    public Transform minPoint,maxPoint;

    public GrowBlock baseGridBlock;

    private Vector2Int gridSize;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateGrid()
    {
        //Of car on est en 2D, on met z a 0
        minPoint.position = new Vector3(Mathf.Round(minPoint.position.x), Mathf.Round(minPoint.position.y), 0f);
        maxPoint.position = new Vector3(Mathf.Round(maxPoint.position.x), Mathf.Round(maxPoint.position.y), 0f);


        Vector3 startPoint = minPoint.position + new Vector3(0.5f,0.5f,0f);

        //Test
        //Instantiate(baseGridBlock,startPoint,Quaternion.identity);
        
        gridSize = new Vector2Int(Mathf.RoundToInt(maxPoint.position.x - minPoint.position.x), Mathf.RoundToInt(maxPoint.position.y - minPoint.position.y));

    }
}
