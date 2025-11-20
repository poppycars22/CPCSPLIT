using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using HarmonyLib;

using UnboundLib;
using UnboundLib.Cards;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;

using UnityEngine;
using TMPro;
using RarityLib.Utils;
using UnboundLib.Utils;

namespace CPCCore.Utilities
{
    internal class Miscs
    {
        public static bool debugFlag = true;

        public static void Log(object message)
        {
            if (debugFlag)
            {
                UnityEngine.Debug.Log(message);
            }
        }

        public static void LogWarn(object message)
        {
            if (debugFlag)
            {
                UnityEngine.Debug.LogWarning(message);
            }
        }

        public static void LogError(object message)
        {
            if (debugFlag)
            {
                UnityEngine.Debug.LogError(message);
            }
        }

        

        // String Utils
        public static List<string> StringSplit(string input, char splitAt)
        {
            List<string> result = new List<string>();
            string buffer = "";

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != splitAt)
                {
                    buffer += input[i];
                }
                else
                {
                    result.Add(buffer);
                    buffer = "";
                }
            }
            if (buffer != "")
            {
                result.Add(buffer);
            }

            return result;
        }

        public static int ValidateStringQuery(string targetString, string query)
        {
            // prioritize literal accuracy rather than get the first hit (that is just totally off-target match)
            int baseWeight = 15;
            int queryIndex = 0;
            int result = 0;
            bool flagPerfectMatch = true;

            for (int i = 0; i < targetString.Length; i++)
            {
                if (targetString[i] == query[queryIndex])
                {
                    result += baseWeight;
                    queryIndex++;
                }
                else
                {
                    flagPerfectMatch = false;
                    baseWeight--;
                    queryIndex++;
                }

                if (queryIndex == query.Length)
                {
                    if (flagPerfectMatch)
                    {
                        result += baseWeight * 10;
                    }
                    break;
                }
            }

            return result;
        }
    }
        public class CardUtils
    {


        public class PlayerCardData
        {
            public CardInfo cardInfo;
            public Player owner;
            public int index;

            public PlayerCardData(CardInfo cardInfo, Player owner, int index)
            {
                this.cardInfo = cardInfo;
                this.owner = owner;
                this.index = index;
            }
        }

        public static bool PlayerHasCard(Player player, string cardName)
        {
            List<PlayerCardData> candidate = GetPlayerCardsWithName(player, cardName);
            return candidate.Count > 0;
        }

        public static bool PlayerHasCardCategory(Player player, CardCategory cardCategory)
        {
            List<PlayerCardData> candidate = GetPlayerCardsWithCategory(player, cardCategory);
            return candidate.Count > 0;
        }

        public static List<PlayerCardData> GetPlayerCardsWithName(Player player, string targetCardName)
        {
            targetCardName = targetCardName.ToUpper();
            string checkCardName;

            List<PlayerCardData> candidates = new List<PlayerCardData>();
            List<CardInfo> playerCards = player.data.currentCards;

            for (int i = 0; i < playerCards.Count; i++)
            {
                checkCardName = playerCards[i].cardName.ToUpper();

                if (checkCardName.Equals(targetCardName))
                {
                    candidates.Add(new PlayerCardData(playerCards[i], player, i));
                }
            }
            return candidates;
        }

        public static List<PlayerCardData> GetPlayerCardsWithCategory(Player player, CardCategory targetCategory)
        {
            List<PlayerCardData> candidates = new List<PlayerCardData>();
            List<CardInfo> playerCards = player.data.currentCards;

            for (int i = 0; i < playerCards.Count; i++)
            {
                bool match = false;
                CardCategory[] thisCardCat = CustomCardCategories.instance.GetCategoriesFromCard(playerCards[i]);
                foreach (CardCategory category in thisCardCat)
                {
                    if (targetCategory == category)
                    {
                        match = true;
                        break;
                    }
                }
                if (match)
                {
                    candidates.Add(new PlayerCardData(playerCards[i], player, i));
                }
            }
            return candidates;
        }

        public static CardInfo GetCardInfo(string modInitial, string cardNameExact)
        {
            string queryText = $"__{modInitial}__{cardNameExact}";
            List<CardInfo> cardInfoList = CardManager.cards.Values.Select(c => c.cardInfo).ToList();

            // Miscs.Log("GetCardInfo(exact) " + queryText);
            CardInfo result = null;
            foreach (CardInfo item in cardInfoList)
            {
                if (item.gameObject.name.Contains(queryText))
                {
                    result = item;
                    break;
                }
            }

            // if (result == null)
            // {
            //     Miscs.LogWarn("Cannot find: " + queryText);
            // }
            // else
            // {
            //     Miscs.Log("Found: " + result.name);
            // }
            return result;
        }

        public static CardInfo GetCardInfo(string query, bool searchVanillaOnly = false)
        {
            //would return the first hit in list of cards
            if (query.Contains("@"))
            {
                List<string> splitQuery = Miscs.StringSplit(query, '@');

                // foreach (var item in splitQuery)
                // {
                //     Miscs.Log("#> " + item);
                // }

                if (splitQuery.Count == 2)
                {
                    if (splitQuery[0] == "")
                    {
                        return GetCardInfo(splitQuery[1]);
                    }
                    else if (splitQuery[0] == "Vanilla")
                    {
                        return GetCardInfo(splitQuery[1], true);
                    }
                    return GetCardInfo(splitQuery[0], splitQuery[1]);
                }
                else
                {
                    Miscs.LogWarn(">> query splitting failed");
                    query = splitQuery[0];
                }
            }

            query = CardNameSanitize(query, removeWhitespaces: true);
            string cardName;
            List<CardInfo> cardInfoList = CardManager.cards.Values.Select(c => c.cardInfo).ToList();

            // if (!query.Contains("__"))
            // {
            //     searchVanillaOnly = true;
            // }

            // Miscs.Log("GetCardInfo(query) " + query);
            CardInfo result = null;
            int matchScore = 0;
            foreach (CardInfo item in cardInfoList)
            {
                cardName = CardNameSanitize(item.gameObject.name, removeWhitespaces: true);
                if (searchVanillaOnly && cardName.Contains("__")) continue;
                // Logging here is process time-intensive
                // Miscs.Log($"> Check for [{query}] <-> [{cardName}]");

                cardName = CardNameSanitize(item.cardName, removeWhitespaces: true);

                if (cardName.Equals(query))
                {
                    result = item;
                    matchScore = 9999;
                    break;
                }
                else if (cardName.Contains(query))
                {
                    int newMatch = Miscs.ValidateStringQuery(cardName, query);
                    if (newMatch > matchScore)
                    {
                        matchScore = newMatch;
                        result = item;
                    }
                }
            }

            // if (result == null)
            // {
            //     Miscs.LogWarn("Cannot find: " + query);
            // }
            // else
            // {
            //     Miscs.Log($"Found: {result.gameObject.name} [{matchScore}]");
            // }
            return result;
        }

        public static List<PlayerCardData> GetPlayerCardsWithCardInfo(Player player, CardInfo cardInfo)
        {
            string targetCardName = cardInfo.gameObject.name.ToUpper();
            string checkCardName;

            List<PlayerCardData> candidates = new List<PlayerCardData>();
            List<CardInfo> playerCards = player.data.currentCards;

            for (int i = 0; i < playerCards.Count; i++)
            {
                checkCardName = playerCards[i].gameObject.name.ToUpper();

                if (checkCardName.Equals(targetCardName))
                {
                    candidates.Add(new PlayerCardData(playerCards[i], player, i));
                }
            }
            return candidates;
        }

        public static List<PlayerCardData> GetPlayerCardsWithStringList(Player player, List<string> checkList)
        {
            List<PlayerCardData> candidates = new List<PlayerCardData>();
            List<CardInfo> playerCards = player.data.currentCards;
            CardInfo tempCardInfo;

            foreach (string item in checkList)
            {
                tempCardInfo = GetCardInfo(item);
                if (tempCardInfo == null) continue;

                candidates = candidates.Concat(GetPlayerCardsWithCardInfo(player, tempCardInfo)).ToList();
            }

            return candidates;
        }
        public static string CardNameSanitize(string name, bool removeWhitespaces = false)
        {
            string result = name.ToLower();
            if (removeWhitespaces)
            {
                result = result.Replace(" ", "");
            }

            return result;
        }

        public static void MakeExclusive(string cardA, string cardB)
        {
            CardInfo cardInfoA, cardInfoB;
            cardInfoA = GetCardInfo(cardA);
            cardInfoB = GetCardInfo(cardB);

            if (cardInfoA != null && cardInfoB != null)
            {
                CustomCardCategories.instance.MakeCardsExclusive(cardInfoA, cardInfoB);

                Miscs.Log($"[CPC] MakeExclusive: card [{cardA}] and card [{cardB}] made exclusive");
            }
            else
            {
                if (cardInfoA == null)
                {
                    Miscs.LogWarn($"[CPC] MakeExclusive: card [{cardA}] not found");
                }
                if (cardInfoB == null)
                {
                    Miscs.LogWarn($"[CPC] MakeExclusive: card [{cardB}] not found");
                }
            }
        }
    }
}