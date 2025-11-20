using ClassesManagerReborn;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib.Cards;
using UnboundLib.GameModes;

namespace CPCCrafter.Cards
{
    class MinecrafterClass : ClassHandler
    {
        internal static string name = "Minecrafter";

        public override IEnumerator Init()
        {
            UnityEngine.Debug.Log("Regestering: " + name);
            while (!(CraftingTable.Card && MCBow.Card && MCShield.Card && WoodenSword.Card && StoneSword.Card && IronSword.Card && GoldSword.Card && DiamondSword.Card && NetheriteSword.Card && WoodenAxe.Card && StoneAxe.Card && IronAxe.Card && GoldAxe.Card && DiamondAxe.Card && NetheriteAxe.Card && WoodenHoe.Card && StoneHoe.Card && IronHoe.Card && GoldHoe.Card && DiamondHoe.Card && NetheriteHoe.Card && LetherArmor.Card && ChainArmor.Card && IronArmor.Card && GoldArmor.Card && DiamondArmor.Card && NetheriteArmor.Card && BrewingStand.Card && InvisablityPotion.Card && JumpPotion.Card && RegenPotion.Card && SpeedPotion.Card && StrengthPotion.Card && TotemOfUndying.Card && UltimatePotion.Card && DamageArrows.Card && RedstoneDust.Card && GlowstoneDust.Card)) yield return null;
            ClassesRegistry.Register(CraftingTable.Card, CardType.Entry);
            ClassesRegistry.Register(MCShield.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(AutoCrafter.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(WoodenSword.Card, CardType.Gate, CraftingTable.Card);
                ClassesRegistry.Register(StoneSword.Card, CardType.Gate, WoodenSword.Card);
                    ClassesRegistry.Register(IronSword.Card, CardType.Gate, StoneSword.Card);
            ClassesRegistry.Register(GoldSword.Card, CardType.Card, CraftingTable.Card);
                        ClassesRegistry.Register(DiamondSword.Card, CardType.Gate, IronSword.Card);
                            ClassesRegistry.Register(NetheriteSword.Card, CardType.Card, DiamondSword.Card);
            ClassesRegistry.Register(WoodenAxe.Card, CardType.Gate, CraftingTable.Card);
                ClassesRegistry.Register(StoneAxe.Card, CardType.Gate, WoodenAxe.Card);
                    ClassesRegistry.Register(IronAxe.Card, CardType.Gate, StoneAxe.Card);
            ClassesRegistry.Register(GoldAxe.Card, CardType.Card, CraftingTable.Card);
                        ClassesRegistry.Register(DiamondAxe.Card, CardType.Gate, IronAxe.Card);
                            ClassesRegistry.Register(NetheriteAxe.Card, CardType.Card, DiamondAxe.Card);
            ClassesRegistry.Register(WoodenHoe.Card, CardType.Gate, CraftingTable.Card);
                ClassesRegistry.Register(StoneHoe.Card, CardType.Gate, WoodenHoe.Card);
                    ClassesRegistry.Register(IronHoe.Card, CardType.Gate, StoneHoe.Card);
            ClassesRegistry.Register(GoldHoe.Card, CardType.Card, CraftingTable.Card);
                        ClassesRegistry.Register(DiamondHoe.Card, CardType.Gate, IronHoe.Card);
                            ClassesRegistry.Register(NetheriteHoe.Card, CardType.Card, DiamondHoe.Card);
            ClassesRegistry.Register(LetherArmor.Card, CardType.Gate, CraftingTable.Card);
                ClassesRegistry.Register(ChainArmor.Card, CardType.Gate, LetherArmor.Card);
                    ClassesRegistry.Register(IronArmor.Card, CardType.Gate, ChainArmor.Card);
            ClassesRegistry.Register(GoldArmor.Card, CardType.Card, CraftingTable.Card);
                        ClassesRegistry.Register(DiamondArmor.Card, CardType.Gate, IronArmor.Card);
                            ClassesRegistry.Register(NetheriteArmor.Card, CardType.Card, DiamondArmor.Card);
            
            ClassesRegistry.Register(TotemOfUndying.Card, CardType.Card, CraftingTable.Card, CardLimit: 5);
            ClassesRegistry.Register(BrewingStand.Card, CardType.SubClass, CraftingTable.Card);
                ClassesRegistry.Register(InvisablityPotion.Card, CardType.Card, BrewingStand.Card);
                ClassesRegistry.Register(JumpPotion.Card, CardType.Card, BrewingStand.Card);
                ClassesRegistry.Register(RegenPotion.Card, CardType.Card, BrewingStand.Card);
                ClassesRegistry.Register(SpeedPotion.Card, CardType.Card, BrewingStand.Card);
                ClassesRegistry.Register(StrengthPotion.Card, CardType.Card, BrewingStand.Card);
            ClassesRegistry.Register(UltimatePotion.Card, CardType.Card, BrewingStand.Card);
            ClassesRegistry.Register(GlowstoneDust.Card, CardType.Card, BrewingStand.Card);
            ClassesRegistry.Register(RedstoneDust.Card, CardType.Card, BrewingStand.Card);
            ClassesRegistry.Register(MCBow.Card, CardType.SubClass, CraftingTable.Card);
                //ClassesRegistry.Register(FlamingArrows.Card, CardType.Card, MCBow.Card);
                ClassesRegistry.Register(PoisonArrows.Card, CardType.Card, MCBow.Card);
                ClassesRegistry.Register(BouncyArrows.Card, CardType.Card, MCBow.Card);
                ClassesRegistry.Register(ToxicArrows.Card, CardType.Card, MCBow.Card);
                ClassesRegistry.Register(Arrow.Card, CardType.Card, MCBow.Card);
                ClassesRegistry.Register(ExplosiveArrows.Card, CardType.Card, MCBow.Card);
                ClassesRegistry.Register(PunchII.Card, CardType.Card, MCBow.Card);
                ClassesRegistry.Register(DamageArrows.Card, CardType.Card, MCBow.Card, CardLimit: 3);
            
           
            //Crossbow (much faster bullets, slower attack cooldown)
            //Levitation tipped arrows
            
            //Modded subclass
            //MOST OF THESE WILL BE RARES OR HIGHER <--- nearly no ideas
            
            
            //Redstoner subclass <--- no ideas
           
            
            //Builder subclass
            //Dirt, build a box that is easily broken
            //Stone, build a box that is slightly harder to break
            //Deepslate
            //Obsidian
            //(tier system)
            
            //Farmer subclass
            //potato gain a bit of regen
            //potato enjoyer, gives 5 potatos
            //#3 potato farmer, gives 10 potatos
            //Technoblade, gives a potato every other round
            //bread gain a bit more regen
            //steak gain even more regen
            //golden carrot gain a lot of regen

        }
        public override IEnumerator PostInit()
        {
            yield break;
        }
    }
}