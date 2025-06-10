using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private Transform content;
    [SerializeField]
    private Button buttonPrefab;
    [SerializeField]
    private Button closeButton;
    private Dictionary<Button, ItemData> itemSlotButtons = new Dictionary<Button, ItemData>();
    protected UIManager uiManager;
    protected GameContext gameContext;

    public virtual void Initialize(GameContext gameContext, UIManager uiManager)
    {
        this.gameContext = gameContext;
        this.uiManager = uiManager;
        closeButton.onClick.AddListener(() =>
        {
            uiManager.CloseInventoryUI();
            Time.timeScale = 1.0f;
        });
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        foreach (ItemData itemData in gameContext.player.playerData.inventory)
        {
            ItemData selectedItemData = itemData;
            Button itemSlotButton = Instantiate(buttonPrefab, content);
            if (itemSlotButton.TryGetComponent<ItemSlot>(out ItemSlot itemSlot))
            {
                itemSlot.Initialize(gameContext, selectedItemData);
                itemSlotButton.onClick.AddListener(() =>
                {
                    PlayerData playerData = gameContext.player.playerData;
                    int resultHp = Mathf.Min(playerData.hp + selectedItemData.addHp, playerData.maxHp);
                    resultHp = Mathf.Max(resultHp, 1);
                    playerData.hp = resultHp;
                    int resultGold = Mathf.Max(playerData.gold - itemData.goldCost, 0);
                    playerData.gold = resultGold;
                });
                itemSlotButtons.Add(itemSlotButton, selectedItemData);
            }
        }
    }

    private void OnDisable()
    {
        foreach (var kvp in itemSlotButtons)
        {
            Destroy(kvp.Key.gameObject);
        }
        itemSlotButtons.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
