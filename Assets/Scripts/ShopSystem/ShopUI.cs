using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public int totalCoins;
    PlayerScore playerScore;

    public ShopSaveScriptable shopData;
    public GameObject[] playerModels;
    public TextMeshProUGUI unlockBtnText, upgradeBtnText, levelText, playerNameText;
    public TextMeshProUGUI healthText, speedText, jumpText, totalCoinsText;
    public Button unlockBtn, upgradeBtn, nextBtn, previousBtn;

    private int currentIndex = 0;
    private int selectedINdex = 0;
    // private int totalCoins = 0;

    private void Start()
    {
        playerScore = FindObjectOfType<PlayerScore>();
        selectedINdex = shopData.selectedIndex;
        currentIndex = selectedINdex;
        totalCoins = playerScore.GetPlayerScore();
        Debug.Log("Total coins: " + totalCoins);
        totalCoinsText.text = "" + totalCoins;
        setPlayerInfor();

        unlockBtn.onClick.AddListener(() => UnlockSelectedBtnMethod());
        upgradeBtn.onClick.AddListener(() => UpgradeBtnMethod());
        nextBtn.onClick.AddListener(() => NextBtnMethod());
        previousBtn.onClick.AddListener(() => PreviousBtnMethod());

        playerModels[currentIndex].SetActive(true);

        if (currentIndex == 0)
        {
            previousBtn.interactable = false;
        }
        if (currentIndex == shopData.shopItems.Length - 1)
        {
            nextBtn.interactable = false;
        }

        UnlockBtnStatus();
        UpgradeBtnStatus();

    }

    private void setPlayerInfor()
    {
        playerNameText.text = shopData.shopItems[currentIndex].itemName;
        int currentLevel = shopData.shopItems[currentIndex].unlockedLevel;

        levelText.text = "Level: " + (currentLevel + 1);
        healthText.text = "Health: " + shopData.shopItems[currentIndex].playerLevel[currentLevel].Health;
        speedText.text = "Speed: " + shopData.shopItems[currentIndex].playerLevel[currentLevel].moveSpeed;
        jumpText.text = "Jump: " + shopData.shopItems[currentIndex].playerLevel[currentLevel].jumpSpeed;


    }

    public void NextBtnMethod()
    {
        if (currentIndex < shopData.shopItems.Length - 1)
        {
            playerModels[currentIndex].SetActive(false);
            currentIndex++;
            playerModels[currentIndex].SetActive(true);
            setPlayerInfor();

            if (currentIndex == shopData.shopItems.Length - 1)
            {
                nextBtn.interactable = false;
            }
            if (!previousBtn.interactable)
            {
                previousBtn.interactable = true;
            }

            UnlockBtnStatus();
            UpgradeBtnStatus();
        }
    }

    public void PreviousBtnMethod()
    {
        if (currentIndex > 0)
        {
            playerModels[currentIndex].SetActive(false);
            currentIndex--;
            playerModels[currentIndex].SetActive(true);
            setPlayerInfor();
            if (currentIndex == 0)
            {
                previousBtn.interactable = false;
            }
            if (!nextBtn.interactable)
            {
                nextBtn.interactable = true;
            }

            UnlockBtnStatus();
            UpgradeBtnStatus();
        }
    }

    private void UnlockSelectedBtnMethod()
    {
        bool yesSelected = false;
        if (shopData.shopItems[currentIndex].isUnlocked)
        {
            yesSelected = true;
        }
        else
        {
            if (totalCoins >= shopData.shopItems[currentIndex].unlockCost)
            {
                // totalCoins -= shopData.shopItems[currentIndex].unlockCost;
                
                playerScore.AddPlayerScore(-shopData.shopItems[currentIndex].unlockCost); // Deduct score
                totalCoins = playerScore.GetPlayerScore(); // Update local coins
    
                totalCoinsText.text = "" + totalCoins;
                shopData.shopItems[currentIndex].isUnlocked = true;
                yesSelected = true;
                UpgradeBtnStatus();
            }
        }
        if (yesSelected)
        {
            unlockBtnText.text = "Selected";
            selectedINdex = currentIndex;
            shopData.selectedIndex = selectedINdex;
            unlockBtn.interactable = false;
            foreach (Character character in GameManager.instance.characters)
            {
                if (character.name == shopData.shopItems[currentIndex].itemName)
                {
                    GameManager.instance.SetCurrentCharacter(character);
                }
            }

        }
    }

    private void UpgradeBtnMethod()
    {
        int nextLevelIndex = shopData.shopItems[currentIndex].unlockedLevel + 1;

        if (totalCoins >= shopData.shopItems[currentIndex].playerLevel[nextLevelIndex].unlockCost)
        {
            // totalCoins -= shopData.shopItems[currentIndex].playerLevel[nextLevelIndex].unlockCost;
            
            playerScore.AddPlayerScore(-shopData.shopItems[currentIndex].playerLevel[nextLevelIndex].unlockCost); // Deduct score
            totalCoins = playerScore.GetPlayerScore();

            totalCoinsText.text = "" + totalCoins;
            shopData.shopItems[currentIndex].unlockedLevel++;

            if (shopData.shopItems[currentIndex].unlockedLevel < shopData.shopItems[currentIndex].playerLevel.Length - 1)
            {
                upgradeBtnText.text = "UpgradedCost" +
                shopData.shopItems[currentIndex].playerLevel[nextLevelIndex + 1].unlockCost;
            }
            else
            {

                upgradeBtn.interactable = false;
                upgradeBtnText.text = "Max Level";
            }

            setPlayerInfor();

        }
    }

    private void UnlockBtnStatus()
    {
        if (shopData.shopItems[currentIndex].isUnlocked)
        {
            // unlockBtn.interactable = selectedINdex != currentIndex;// nếu selectedINdex khác currentIndex thì unlockBtn sẽ được kích hoạt
            if (selectedINdex == currentIndex)
            {
                unlockBtn.interactable = false;
            }
            else
            {
                unlockBtn.interactable = true;
            }
            unlockBtnText.text = selectedINdex == currentIndex ? "Selected" : "Select";
        }
        else
        {
            unlockBtn.interactable = true;
            unlockBtnText.text = "Cost " + shopData.shopItems[currentIndex].unlockCost;
        }
    }

    private void UpgradeBtnStatus()
    {
        if (shopData.shopItems[currentIndex].isUnlocked)
        {
            if (shopData.shopItems[currentIndex].unlockedLevel < shopData.shopItems[currentIndex].playerLevel.Length - 1)
            {
                int nextLevelIndex = shopData.shopItems[currentIndex].unlockedLevel + 1;
                upgradeBtn.interactable = true;
                upgradeBtnText.text = "Upgrade Cost " +
                shopData.shopItems[currentIndex].playerLevel[nextLevelIndex].unlockCost;
            }
            else
            {
                upgradeBtn.interactable = false;
                upgradeBtnText.text = "Max Level";
            }
        }
        else
        {
            upgradeBtn.interactable = false;
            upgradeBtnText.text = "Locked";
        }
    }

}

