using UnityEngine;
using System.Collections.Generic;

public class TilesController : MonoBehaviour {

    public delegate void DefiningCharacters();
    public static event DefiningCharacters OnDefiningCharacters;

    [SerializeField]
    Tile tilePrefab;

    [SerializeField]
    int horizontalLength;
    [SerializeField]
    int verticalLength;

    [SerializeField]
    [Range(5, 8)]
    int baseDistance;

    List<Tile> tiles;    


    void Awake() {
        InitTiles();

        if (OnDefiningCharacters != null) OnDefiningCharacters();
    }

    void InitTiles() {
        tiles = new List<Tile>();

        for (int i = 0; i < horizontalLength; i++) {
            for (int j = 0; j < verticalLength; j++) {
                var vector = new Vector3(i * baseDistance, 0, j * baseDistance);

                Tile auxTile = Instantiate(tilePrefab, vector, Quaternion.identity) as Tile;
                auxTile.SetGridPosition(vector);
                tiles.Add(auxTile);

                auxTile.transform.SetParent(transform);
            }
        }
    }
}
