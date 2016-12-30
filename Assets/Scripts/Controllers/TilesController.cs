using UnityEngine;
using System.Collections.Generic;
using Common.Layout;

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

    public List<Tile> Tiles {
        get {
            return tiles;
        }
    }

    void Awake() {
        Character.OnDefiningPaths += SelectPath;
        Character.OnVerifyClick += VerifyCharacterTile;
        Tile.OnClearTiles += ClearAllTiles;

        InitTiles();
        if (OnDefiningCharacters != null) OnDefiningCharacters();
    }

    void OnDestroy() {
        Character.OnVerifyClick -= VerifyCharacterTile;
        Character.OnDefiningPaths -= SelectPath;
        Tile.OnClearTiles -= ClearAllTiles;
    }

    Tile VerifyCharacterTile(Vector3 position) {        
        return tiles.Find(tile => tile.GridPosition == (position));        
    }

    void SelectPath(Vector3 postionCharacter, List<Vector3> paths, bool activePaths) {
        ClearAllTiles();

        foreach (var path in paths) {
            var currentTile = tiles.Find(tile => tile.GridPosition == (path + postionCharacter) );

            if (currentTile != null) {
                if (activePaths) {
                    currentTile.ActiveAvaliableArea();
                }
            }else {
                //Debug.LogError("Don't contains Tile!");
            }
        }
    }

    void ClearAllTiles() {
        foreach (var tile in tiles) {
            tile.DesactiveAvaliableArea();
        }
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
