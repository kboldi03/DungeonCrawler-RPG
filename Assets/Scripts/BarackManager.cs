using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using NUnit.Framework.Constraints;

public class BaracksManager : MonoBehaviour
{
    [Header("Party Panel")]
    public Transform partyPanel;
    public GameObject partyIconPrefab;

    [Header("Inventory Panel")]
    public GameObject inventoryPanel;
    public Transform equippedItemContainer;
    public Transform inventoryListContent;
    public GameObject itemCardPrefab;
    public Button closeInventoryButton;
    public Button itemSlotButton;

    private CharacterSaveData selectedCharacter;
    private List<GameObject> inventoryCards = new List<GameObject>();

    [Header("Display Panel")]
    public GameObject displayPanel;
    public Image characterSprite;
    public TMP_Text nameText;
    public TMP_Text classText;
    public TMP_Text statsText;
    public Transform skillsPanel;
    public GameObject skillButtonPrefab;

    public Image itemIcon;
    public TMP_Text itemNameText;
    public TMP_Text itemDescText;
    public ItemData[] allItems;
    public Button removeItemButton;

    [Header("Classes")]
    public ClassData[] allClasses;

    private List<GameObject> skillButtons = new List<GameObject>();

    void Start()
    {
        
        displayPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        itemNameText.text = "No item equpped";
        itemDescText.text = "";
        itemIcon.sprite = null;

        itemSlotButton.onClick.AddListener(OpenInventory);
        closeInventoryButton.onClick.AddListener(() => inventoryPanel.SetActive(false));
        removeItemButton.onClick.AddListener(RemoveEquippedItem);

        SpawnPartyIcons();
    }

    void SpawnPartyIcons()
    {
        foreach (CharacterSaveData member in GameManager.instance.party)
        {
            GameObject icon = Instantiate(partyIconPrefab, partyPanel);
            CharacterSaveData captured = member;

            ClassData classData = FindClassData(member.className);
            if (classData != null)
            {
                Image iconImage = icon.GetComponentInChildren<Image>();
                iconImage.sprite = classData.sprite;
            }

            Button button = icon.GetComponent<Button>();
            button.onClick.AddListener(() => ShowMember(captured));
        }
    }

    void ShowMember(CharacterSaveData data)
    {
        selectedCharacter = data;
        displayPanel.SetActive(true);

        UpdateHeader(data);
        UpdateStats(data);
        UpdateSkillButtons(data);
        UpdateEquippedItem(data);

    }
    void UpdateHeader(CharacterSaveData data)
    {
        ClassData classData = FindClassData(data.className);

        if (classData != null)
        {
            characterSprite.sprite = classData.sprite;
        }

        nameText.text = data.characterName;
        classText.text = data.className;

    }

    void UpdateStats(CharacterSaveData data)
    {
        statsText.text =
            "Level: " + data.level + "\n" +
            "Experience points: " + data.currentXP + " / " + data.xpToNextLevel + "\n" +
            "Maximum health points: " + data.GetFinalStat(StatType.MaxHP) + "\n" +
            "Current health points: " + data.currentHP + "\n" +
            "Attack power: " + data.GetFinalStat(StatType.Attack) + "\n"  +
            "Magic power: " + data.GetFinalStat(StatType.Magic) + "\n" +
            "Armor: " + data.GetFinalStat(StatType.Armor) + "\n" +
            "Resistance: " + data.GetFinalStat(StatType.Resistance) + "\n" +
            "Speed: " + data.GetFinalStat(StatType.Speed) + "\n" +
            "Critical hit chance: " + data.GetFinalStat(StatType.Crit) + "%" + "\n" +
            "Armor Penetration: " + data.GetFinalStat(StatType.ArmorPen) + "\n" +
            "Magic Penetration: " + data.GetFinalStat(StatType.MagicPen) + "\n";

    }

    void UpdateSkillButtons(CharacterSaveData data)
    {
        ClearSkillButtons();
        ClassData classData = FindClassData(data.className);

        if (classData == null) return;

        foreach (SkillData skill in classData.skills)
        {
            GameObject btn = Instantiate(skillButtonPrefab, skillsPanel);
            skillButtons.Add(btn);

            TMP_Text[] texts = btn.GetComponentsInChildren<TMP_Text>();
            foreach (TMP_Text text in texts)
            {
                if (text.gameObject.name == "Skill Name")
                    text.text = skill.skillName;
                else if (text.gameObject.name == "Description")
                    text.text = skill.description;
            }

            // set icon
            Image[] images = btn.GetComponentsInChildren<Image>();
            foreach (Image img in images)
            {
                if (img.gameObject.name == "Icon")
                {
                    img.sprite = skill.icon;
                    break;
                }
            }
        }

    }

    void UpdateEquippedItem(CharacterSaveData data)
    {
        ItemInstance equippedItem = data.equippedItem;
        if (equippedItem != null)
        {
            ItemData itemData = FindItem(data.equippedItem.itemName);
            if(itemData != null)
                itemIcon.sprite = itemData.icon;

            itemNameText.text = equippedItem.itemName;
            itemDescText.text = BuildStatsText(equippedItem);
            removeItemButton.gameObject.SetActive(true);
        }
        else
        {
            itemIcon.sprite = null;
            itemNameText.text = "No item equipped";
            itemDescText.text = "";
            removeItemButton.gameObject.SetActive(false);
        }
    }
    ItemData FindItem(string itemName)
    {
        foreach (ItemData item in allItems)
            if (item.itemName == itemName)
                return item;
        return null;
    }

    void ClearSkillButtons()
    {
        foreach (GameObject btn in skillButtons)
            Destroy(btn);
        skillButtons.Clear();
    }

    ClassData FindClassData(string className)
    {
        foreach (ClassData classData in allClasses)
            if (classData.className == className)
                return classData;
        return null;
    }

    void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        RefreshInventoryDisplay();
    }

    void RefreshInventoryDisplay()
    {
        
        foreach(GameObject card in inventoryCards) 
            Destroy(card);
        inventoryCards.Clear();

        foreach (ItemInstance item in GameManager.instance.inventory)
        {
            GameObject card = CreateItemCard(item, inventoryListContent);
            card.GetComponentInChildren<Button>().GetComponentInChildren<TMP_Text>().text = "Equip";
            ItemInstance captured = item;
            card.GetComponentInChildren<Button>().onClick.AddListener(() => EquipItem(captured));
            inventoryCards.Add(card);
        }
    }

    GameObject CreateItemCard(ItemInstance instance, Transform parent)
    {
        GameObject card = Instantiate(itemCardPrefab, parent);

        ItemData itemData = FindItem(instance.itemName);
        if (itemData != null)
            card.transform.Find("Icon").GetComponent<Image>().sprite = itemData.icon;

        card.transform.Find("Name").GetComponent<TMP_Text>().text = instance.itemName;
        card.transform.Find("Stats").GetComponent<TMP_Text>().text = BuildStatsText(instance);

        return card;
    }

    string BuildStatsText(ItemInstance instance)
    {
        string text = "";
        foreach(RolledModifier mod in instance.rolledModifiers)
        {
            string sign = mod.value >= 0 ? "+" : "";
            text += mod.stat + ": " + sign + mod.value + "\n";
        }
        return text.TrimEnd();
    }

    void EquipItem(ItemInstance newItem)
    {
        if (selectedCharacter.equippedItem != null)
        {
            GameManager.instance.inventory.Add(selectedCharacter.equippedItem);
        }
        selectedCharacter.equippedItem = newItem;
        GameManager.instance.inventory.Remove(newItem);

        RefreshInventoryDisplay();
        UpdateStats(selectedCharacter);
        UpdateEquippedItem(selectedCharacter);
    }

    void RemoveEquippedItem()
    {
        GameManager.instance.inventory.Add(selectedCharacter.equippedItem);
        selectedCharacter.equippedItem = null;

        RefreshInventoryDisplay();
        UpdateStats(selectedCharacter);
        UpdateEquippedItem(selectedCharacter);
    }
}