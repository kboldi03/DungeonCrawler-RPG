using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BlacksmithManager : MonoBehaviour
{
    [Header("Panel")]
    public Transform itemPanel;
    public GameObject itemCardPrefab;

    [Header("Items")]
    public List<ItemData> allItems = new List<ItemData>();

    private List<GameObject> itemCards = new List<GameObject>();

    void Start()
    {
        GenerateItems();
    }

    void GenerateItems()
    {
        

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, allItems.Count);
            ItemData item = allItems[randomIndex];

            ItemInstance instance = RollItem(item);

            GameObject card = Instantiate(itemCardPrefab, itemPanel);
            itemCards.Add(card);

            card.transform.Find("Icon").GetComponent<Image>().sprite = item.icon;
            card.transform.Find("Name").GetComponent<TMP_Text>().text = item.itemName;
            card.transform.Find("Stats").GetComponent<TMP_Text>().text = BuildStatsText(instance);

            card.GetComponentInChildren<Button>().onClick.AddListener(() => BuyItem(instance, card));
        }
    }

    ItemInstance RollItem(ItemData template)
    {
        ItemInstance instance = new ItemInstance();
        instance.itemName = template.itemName;

        foreach(StatModifier mod  in template.modifiers)
        {
            RolledModifier rolled = new RolledModifier();
            rolled.stat = mod.stat;
            rolled.value = Random.Range(mod.minValue, mod.maxValue+1);
            instance.rolledModifiers.Add(rolled);
        }
        return instance;
    }

    string BuildStatsText(ItemInstance instance)
    {
        string text = "";
        foreach (RolledModifier mod in instance.rolledModifiers)
        {
            
            string sign = mod.value >= 0 ? "+" : "";
            text += mod.stat + ": " + sign + mod.value + "\n";
            
        }
        return text.TrimEnd();
    }

    void BuyItem(ItemInstance instance, GameObject card)
    {
        GameManager.instance.inventory.Add(instance);
        card.transform.localScale = Vector3.zero;
    }
}