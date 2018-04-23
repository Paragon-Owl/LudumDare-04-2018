using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropField : MonoBehaviour {

	private Tilemap map;
	public List<FieldTile> tiles;

	public Tile baseTile;
	public Tile hoedTile;
	public Tile plantedTile;
	public Tile growingTile;
	public Tile grownTile;


	private bool ready = false;

	// Use this for initialization
	void Start () {
		map = GetComponent<Tilemap>();
		tiles = new List<FieldTile>();
		ready = false;
	}

	private void InitField()
	{
		GameObject gob = new GameObject("Tile");
		gob.AddComponent<FieldTile>();

		for(int i=0;i < map.size.x ;i++)
		{
			for (int j = 0; j < map.size.y; j++)
			{

				Vector3Int pos = new Vector3Int(i,j,0);

				if(!map.HasTile(pos))
					continue;
				Debug.Log(map.size + " / "+ i + " " + j);

				Tile a = (Tile)map.GetTile(pos);
				if(baseTile==a)
				{
					Debug.Log("Found at "+ i + " " + j);
					FieldTile b = Instantiate(gob, map.CellToWorld(pos), Quaternion.identity, transform).GetComponent<FieldTile>();
					b.name = "Tile"+i+"_"+j;
					tiles.Add(b);
					b.tile = pos;
				}
			}
		}
		Debug.Log(tiles.Count);
		ready = true;
	}

	// Update is called once per frame
	void Update () {

		if(!ready)
		{
			InitField();
			return;
		}



		foreach (FieldTile field in tiles)
		{
			if(field.hasChanged())
			{
				Debug.Log("Update");
				DrawTile(field.tile, field.currentState);

			}
		}
	}

	private void DrawTile(Vector3Int position, FieldTile.State state)
	{
		switch (state)
		{
			case FieldTile.State.EMPTY:
				map.SetTile(position, baseTile);
				break;
			case FieldTile.State.HOED:
				map.SetTile(position, hoedTile);
				return;
			case FieldTile.State.PLANTED:
				map.SetTile(position, plantedTile);
				break;
			case FieldTile.State.GROWING:
				map.SetTile(position, growingTile);
				break;
			case FieldTile.State.GROWN:
				map.SetTile(position, grownTile);
				break;
			default:
				return;
		}
	}
}
