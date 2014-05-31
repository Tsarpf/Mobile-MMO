using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;


class Floor
{
    Material[] floorMats;
    private int sizeX, sizeY;
    GameObject[][] cells;
    public Floor(Material defaultMaterial, Material gridMaterial, int sizeX, int sizeY)
    {
        floorMats = new Material[2];
        floorMats[0] = gridMaterial;
        floorMats[1] = defaultMaterial;

        this.sizeX = sizeX;
        this.sizeY = sizeY;

        initializeCells();
    }
    private void initializeCells()
    {
        cells = new GameObject[sizeX][];
        Material[] mats = floorMats;
        for (int x = 0; x < sizeX; x++)
        {
            cells[x] = new GameObject[sizeY];  
            for (int y = 0; y < sizeY; y++)
            {
                cells[x][y] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cells[x][y].AddComponent<Cell>();
                cells[x][y].GetComponent<Cell>().Initialize(mats, new Vector3(x, 0, y), cells[x][y]);
            }
        }
    }
    public void SetPosition(Vector3 position)
    {
        throw new NotImplementedException();
    }
}
