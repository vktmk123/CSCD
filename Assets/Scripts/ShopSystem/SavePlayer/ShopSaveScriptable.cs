using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "ShopData", menuName = "Scriptable/Create ShopData")]
    public class ShopSaveScriptable : ScriptableObject
        {
        public int selectedIndex;
        public ShopItem[] shopItems;
    }

    [System.Serializable]
    public class ShopItem{
        public string itemName;
        public bool isUnlocked;
        public int unlockCost;
        public int unlockedLevel;
        public PlayerInfor[] playerLevel;
    }

     [System.Serializable]
    public class PlayerInfor{
        public int unlockCost;
        public float Health;
        public float moveSpeed;
        public float jumpSpeed;
    }

