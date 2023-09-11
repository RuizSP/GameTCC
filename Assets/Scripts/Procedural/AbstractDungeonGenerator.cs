using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public abstract class AbstractDungeonGenerator : MonoBehaviour
{ 
    [SerializeField] protected TileMapVisualizer tilemapVisualizer = null;
    //[SerializeField] protected Vector2Int startPosition = Vector2Int.zero;
    protected Vector2Int startPosition;

    public void GenerateDungeon(){
        tilemapVisualizer.Clear();
        RunProceduralGeneration();
    }

    protected abstract void  RunProceduralGeneration();
    
}