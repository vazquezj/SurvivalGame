using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {

	public List<Item> items = new List<Item> ();

	void Start ()
	{
		items.Add (new Item ("Food", 0, "Food", Item.ItemType.Consumable));
		items.Add (new Item ("Water", 1, "Water", Item.ItemType.Consumable));
	}

	public Item GetItem(int id)
	{
		for (int i=0; i<items.Count; i++)
		{
			if (items[i].itemID == id)
			{
				return items[i];
			}
		}
		return null;
	}﻿
}