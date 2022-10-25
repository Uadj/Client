	using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MapEditor
{

#if UNITY_EDITOR

	[MenuItem("Tools/GenerateMap2 %#g")]
	private static void Hello()
    {
		GameObject go = GameObject.Find("Map");
        if (go == null)
        {
			return;
        }
		Tilemap tm = Util.FindChild<Tilemap>(go, "Tilemap_Collision", true);
		if (tm == null)
		{
			return;
		}
		
		//txt
		using(var writer = File.CreateText("Assets/Resources/Map/output.txt"))
        {
			writer.WriteLine(tm.cellBounds.xMin);
			writer.WriteLine(tm.cellBounds.xMax);
			writer.WriteLine(tm.cellBounds.yMin);
			writer.WriteLine(tm.cellBounds.yMax);

			int count = 0;
			for(int y=tm.cellBounds.yMax; y >= tm.cellBounds.yMin; y--)
            {	
				for (int x = tm.cellBounds.xMin; x <= tm.cellBounds.xMax; x++)
                {
					count++;
					if (count > 1000) return;
					TileBase tile = tm.GetTile(new Vector3Int(x, y, 0));
					if (tile != null)
						writer.Write("1");
					else
						writer.Write("0");
                }
				writer.WriteLine();
			}
		}
	}
	// % (Ctrl), # (Shift), & (Alt)

	[MenuItem("Tools/GenerateMap %#g")]
	private static void GenerateMap()
	{
		GenerateByPath("Assets/Resources/Map");
		GenerateByPath("../Common/MapData");
	}

	private static void GenerateByPath(string pathPrefix)
	{
		GameObject[] gameObjects = Resources.LoadAll<GameObject>("Prefabs/Map");

		foreach (GameObject go in gameObjects)
		{
			Tilemap tmBase = Util.FindChild<Tilemap>(go, "Tilemap_Base", true);
			Tilemap tm = Util.FindChild<Tilemap>(go, "Tilemap_Collision", true);

			using (var writer = File.CreateText($"{pathPrefix }/{go.name}.txt"))
			{
				writer.WriteLine(tmBase.cellBounds.xMin);
				writer.WriteLine(tmBase.cellBounds.xMax);
				writer.WriteLine(tmBase.cellBounds.yMin);
				writer.WriteLine(tmBase.cellBounds.yMax);

				for (int y = tmBase.cellBounds.yMax; y >= tmBase.cellBounds.yMin; y--)
				{
					for (int x = tmBase.cellBounds.xMin; x <= tmBase.cellBounds.xMax; x++)
					{
						
						TileBase tile = tm.GetTile(new Vector3Int(x, y, 0));
						if (tile != null)
							writer.Write("1");
						else
							writer.Write("0");
					}
					writer.WriteLine();
				}
			}
		}
	}

#endif

}
