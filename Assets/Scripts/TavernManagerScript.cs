using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class TavernManager : MonoBehaviour
{
    [Header("References")]
    public GameObject recruitCardPrefab;
    public Transform recruitsPanel;
    public GameObject characterPrefab;

    public NameDatabase nameDatabase;

    [Header("Available Classes")]
    public ClassData[] availableClasses;

    void Start()
    {
        GenerateRecruits();
    }

    void GenerateRecruits()
    {
        for (int i = 0; i < 3; i++)
        { 
            ClassData randomClass = availableClasses[Random.Range(0, availableClasses.Length)];

            CharacterStats stats = EntityFactory.CreateCharacter(randomClass, characterPrefab, nameDatabase);
            stats.gameObject.SetActive(false); 

            GameObject cardObj = Instantiate(recruitCardPrefab, recruitsPanel);
            RecruitCard card = cardObj.GetComponent<RecruitCard>();
            card.Setup(stats);

            CharacterStats capturedStats = stats;
            GameObject capturedCard = cardObj;

            Button hireButton = cardObj.transform.Find("ButtonsPanel/HireButton").GetComponent<Button>();
            hireButton.onClick.AddListener(() => HireRecruit(capturedStats, capturedCard));

        }

    }

    void HireRecruit(CharacterStats stats, GameObject card)
    {
        if(GameManager.instance.party.Count >= 4)
        {
            return;
        }

        GameManager.instance.party.Add(stats.ToSaveData());

        CanvasGroup cg = card.GetComponent<CanvasGroup>();
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
}