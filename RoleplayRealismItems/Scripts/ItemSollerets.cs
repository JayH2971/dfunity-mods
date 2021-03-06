// Project:         RoleplayRealism:Items mod for Daggerfall Unity (http://www.dfworkshop.net)
// Copyright:       Copyright (C) 2020 Hazelnut
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Author:          Hazelnut

using UnityEngine;
using DaggerfallWorkshop.Game.Formulas;
using DaggerfallWorkshop.Game.Items;
using DaggerfallWorkshop.Game.Serialization;

namespace RoleplayRealism
{
    public class ItemSollerets : DaggerfallUnityItem
    {
        public const int templateIndex = 519;

        public ItemSollerets() : base(ItemGroups.Armor, templateIndex)
        {
        }

        // Add mail prefix to name for Iron+ materials.
        public override int CurrentVariant
        {
            set {
                base.CurrentVariant = value;
                if (nativeMaterialValue >= (int)ArmorMaterialTypes.Iron)
                    shortName = ItemHauberk.mail + shortName;
            }
        }

        // Always use leather record for boots.
        public override int InventoryTextureRecord
        {
            get { return 0; }
        }

        // Gets native material value, modifying it to use the 'chain' value for first byte if plate.
        // This fools the DFU code into treating this item as chainmail for forbidden checks etc.
        public override int NativeMaterialValue
        {
            get { return nativeMaterialValue >= (int)ArmorMaterialTypes.Iron ? nativeMaterialValue - 0x0100 : nativeMaterialValue; }
        }

        public override EquipSlots GetEquipSlot()
        {
            return EquipSlots.Feet;
        }

        public override int GetMaterialArmorValue()
        {
            return ItemHauberk.GetChainmailMaterialArmorValue(nativeMaterialValue);
        }

        public override int GetEnchantmentPower()
        {
            float multiplier = FormulaHelper.GetArmorEnchantmentMultiplier((ArmorMaterialTypes)nativeMaterialValue);
            return enchantmentPoints + Mathf.FloorToInt(enchantmentPoints * multiplier);
        }

        public override ItemData_v1 GetSaveData()
        {
            ItemData_v1 data = base.GetSaveData();
            data.className = typeof(ItemSollerets).ToString();
            return data;
        }

    }
}

