using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // The horizontal layout group to use for the inventory
    public HorizontalLayoutGroup layoutGroup;

    // The images to use for each collectible item
    public Image[] itemImages;

    // The collectible items in the scene
    public GameObject[] collectibleItems;

    // A dictionary to track which collectible item is associated with each image
    private Dictionary<GameObject, Image> itemToImageMap;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the item-to-image map
        itemToImageMap = new Dictionary<GameObject, Image>();

        // Set the initial state of the inventory images
        for (int i = 0; i < itemImages.Length; i++)
        {
            itemImages[i].enabled = false;
        }
    }

    // Function to add a collectible item to the inventory
    public void AddItem(GameObject item)
    {
        // Find the first available image in the inventory
        Image availableImage = null;
        for (int i = 0; i < itemImages.Length; i++)
        {
            if (!itemImages[i].enabled)
            {
                availableImage = itemImages[i];
                break;
            }
        }

        // If an available image was found...
        if (availableImage != null)
        {
            // ... add the collectible item to the inventory
            availableImage.enabled = true;
            itemToImageMap[item] = availableImage;

            // Set the sprite of the image to the sprite of the collectible item
            availableImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
        }
    }

    // Function to remove a collectible item from the inventory
    public void RemoveItem(GameObject item)
    {
        // If the collectible item is in the inventory...
        if (itemToImageMap.ContainsKey(item))
        {
            // ... remove it from the inventory
            Image associatedImage = itemToImageMap[item];
            associatedImage.enabled = false;
            itemToImageMap.Remove(item);
        }
    }
}
