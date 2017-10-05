using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public List<Item> inventory = new List<Item> ();
	public List<Item> slots = new List<Item> ();
	public int slotsX, slotsY;
	public GUISkin skin;
	private bool showInventory;
	private ItemDatabase database;

	private bool showTooltip;
	private string tooltip;

	private bool draggingItem;
	private Item draggedItem;
	private int prevIndex;

	void Start ()
	{
		for (int i = 0; i < (slotsX * slotsY); i++)
		{
			slots.Add (new Item ());
			inventory.Add (new Item ());
		}

		database = GameObject.FindGameObjectWithTag ("ItemDatabase").GetComponent<ItemDatabase> ();
		//AddItem (0);
		//AddItem (1);
		//print (InventoryContains(0));
	}

	void Update ()
	{
		if (Input.GetButtonDown("Inventory"))
		{
			showInventory = !showInventory;
		}
	}

	void OnGUI ()
	{
		if (GUI.Button (new Rect(40, 400, 100, 40), "Save"))
		{
			SaveInventory();
		}
		if (GUI.Button (new Rect(40, 450, 100, 40), "Load"))
		{
			LoadInventory();
		}
		tooltip = "";
		GUI.skin = skin;
		if (showInventory)
		{
			DrawInventory ();
			if (showTooltip)
			{
				GUI.Box (new Rect (Event.current.mousePosition.x, Event.current.mousePosition.y, 200, 200), tooltip/*, skin.GetStyle ("Tooltip")*/);
			}
		}
		if (draggingItem)
		{
			GUI.DrawTexture (new Rect (Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), draggedItem.itemIcon);
		}
		for (int i = 0; i < inventory.Count; i++)
		{
			GUI.Label (new Rect(10, i * 20, 200, 50), inventory[i].itemName);
		}
	}

	void DrawInventory ()
	{
		Event currentEvent = Event.current;
		int i = 0;
		for (int y = 0; y < slotsY; y++)
		{
			for (int x = 0; x < slotsX; x++)
			{
				Rect currentRect = new Rect (x * 60, y * 60, 50, 50);
				Item item = slots[i];
				GUI.Box (currentRect, "", skin.GetStyle("Slot"));
				slots [i] = inventory [i];
				if (slots [i].itemName != null)
				{
					GUI.DrawTexture (currentRect, slots [i].itemIcon);
					if (currentRect.Contains (currentEvent.mousePosition))
					{
						CreateToolTip (slots [i]);
						showTooltip = true;
						if (currentEvent.button == 0 && currentEvent.type == EventType.mouseDrag && !draggingItem)
						{
							draggingItem = true;
							prevIndex = i;
							draggedItem = slots [i];
							inventory [i] = new Item ();
						}
						if (currentEvent.type == EventType.mouseUp && draggingItem)
						{
							inventory [prevIndex] = inventory [i];
							inventory [i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}
						/*if (!draggingItem)
						{
							tooltip = GenerateToolTip (inventory [1]);
						}*/
						if (currentEvent.isMouse && currentEvent.type == EventType.mouseDown && currentEvent.button == 1)
						{
							if ((slots[i].itemType == Item.ItemType.Consumable))
							{
								UseConsumable (slots [i], i, true);
							}
						}
					}
				}
				else
				{
					if (currentRect.Contains (currentEvent.mousePosition))
					{
						if (currentEvent.type == EventType.mouseUp && draggingItem)
						{
							inventory [i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}
					}
				}
				if (tooltip == "")
				{
					showTooltip = false;
				}
				i++;
			}
		}
	}

	string CreateToolTip(Item item)
	{
		tooltip = "<color=#ffffff>" + item.itemName + "</color>\n\n" + item.itemDesc;
		return tooltip;
	}

	void RemoveItem (int id)
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			if (inventory [i].itemID == id)
			{
				inventory [i] = new Item ();
				break;
			}
		}
	}

	private void DrawToolTip ()
	{
		float dynamicSize = skin.box.CalcHeight (new GUIContent (tooltip), 200);
		GUI.Box(new Rect(Event.current.mousePosition.x + 10, Event.current.mousePosition.y, 200, dynamicSize), tooltip, skin.GetStyle("ToolTip"));
	}

	/*private string GenerateToolTip (Item item)
	{
		tooltip = "";
		tooltip += "<color=#ffffff><b>" + item.itemName + "</b></color>" + item.itemDesc + "\n\n<color=#ffffff><b>";
		return tooltip;
	}*/

	private void UseConsumable (Item item, int slot, bool deleteItem)
	{
		switch (item.itemID)
		{
		case 0:
			{
				print ("Used" + item.itemName);
				break;
			}
		case 1:
			{
				print ("Used" + item.itemName);
				break;
			}
		}
		if (deleteItem)
		{
			inventory [slot] = new Item ();
		}
	}

	public void AddItem (int id)
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			if (inventory [i].itemName == null)
			{
				for (int j = 0; j < database.items.Count; j++)
				{
					if (database.items [j].itemID == id)
					{
						inventory[i] = database.items[j];
					}
				}
				break;
			}
		}
	}

	bool InventoryContains (int id)
	{
		bool result = false;
		for (int i = 0; i < inventory.Count; i++)
		{
			result = inventory [i].itemID == id;
			if (result)
			{
				break;
			}
		}
		return result;
	}

	void SaveInventory ()
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			PlayerPrefs.SetInt("Inventory " + i, inventory [i].itemID);
		}
	}

	void LoadInventory()
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			inventory[i] = PlayerPrefs.GetInt("Inventory " + i, -1) >= 0 ? database.GetItem (PlayerPrefs.GetInt("Inventory " + i)): new Item();
		}
	}

	public void AddFood ()
	{
		AddItem (0);
	}

	public void AddWater()
	{
		AddItem (1);
	}
}