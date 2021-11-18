using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 
/// </summary>
[SerializeField]
public class Structure : MonoBehaviour
{
    public byte[,] blocks;
    public string name;

    //Should be removed (Test case)
    public WorldData world;


    //Idea 1 give it the byte of used Tiles 

    //Idea 2 search for right Tile

    private void Awake()
    {
        ReadStructureFromTilemap();
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    public void ReadStructureFromTilemap()
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        blocks = new byte[tilemap.editorPreviewSize.x, tilemap.editorPreviewSize.y];

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    blocks[x, y] = world.GetBlockFromTile(tile);
                    Debug.Log(blocks[x, y]);
                    //Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                }
                else
                {
                    Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                }
            }
        }
    }
}
