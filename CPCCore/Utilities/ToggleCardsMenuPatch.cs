/*using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnboundLib;
using UnboundLib.Utils;
using UnboundLib.Utils.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CPCCore.Patches {
    [HarmonyPatch(typeof(ToggleCardsMenuHandler),"Start")]
    public class ToggleCardsMenuPatch {

        public static void Postfix() {
            ChaosPoppycarsCardsCore.Instance.ExecuteAfterSeconds(0.75f, () => {
                UnityEngine.Debug.Log(0);
                List<Transform> catagorys = new List<Transform>();
                UnityEngine.Debug.Log(1);
                Transform categoryContent = (Transform)typeof(ToggleCardsMenuHandler).GetField("categoryContent", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(ToggleCardsMenuHandler.instance);
                UnityEngine.Debug.Log(2);
                Dictionary<string, Transform> scrollViews = new Dictionary<string, Transform>();
                UnityEngine.Debug.Log(3); 
                int count = categoryContent.childCount;
                UnityEngine.Debug.Log(4);
                for (int i = 0; i < count; i++) {
                    Transform child = categoryContent.GetChild(i);
                    scrollViews.Add(child.name, child);
                }
                UnityEngine.Debug.Log(5);
                foreach (var category in CardManager.categories) {
                    catagorys.Add(scrollViews[category]);
                }
                UnityEngine.Debug.Log(6);
                Transform rootCards = scrollViews["CPC"]; 
                catagorys.Remove(rootCards);
                var shefron = GameObject.Instantiate(rootCards.GetComponentInChildren<Toggle>().transform.parent.GetChild(0).gameObject, rootCards.GetComponentInChildren<Toggle>().transform.parent,true);
                shefron.transform.localPosition = new Vector2(60, 2);
                shefron.GetComponent<TextMeshProUGUI>().text = ">";
                shefron.GetComponent<TextMeshProUGUI>().fontSizeMin = 20;
                shefron.GetComponent<TextMeshProUGUI>().color = new Color(1,1,0.6f);
                List<Transform> rootCatagorys = new List<Transform>();
                foreach(var category in scrollViews) {
                    if(category.Key.StartsWith("CPC (")) {
                        rootCatagorys.Add(category.Value);
                        category.Value.gameObject.SetActive(false);
                    }
                }
                catagorys.RemoveAll(rootCatagorys.Contains);

                rootCards.GetComponent<Button>().onClick.AddListener(() => {
                    shefron.GetComponent<TextMeshProUGUI>().text = "v";
                    shefron.GetComponent<TextMeshProUGUI>().color = new Color(0.4f, 1, 0.6f);
                    rootCatagorys.ForEach(catagory => {
                        if(!catagory.gameObject.activeSelf) {
                            catagory.gameObject.SetActive(true);
                            ChaosPoppycarsCardsCore.Instance.ExecuteAfterFrames(2, () => { catagory.localPosition += Vector3.right * 8; });
                        }
                    });
                });

                foreach(var otherCategory in catagorys) {
                    otherCategory.GetComponent<Button>().onClick.AddListener(() => {
                        shefron.GetComponent<TextMeshProUGUI>().text = ">";
                        shefron.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 0.6f);
                        rootCatagorys.ForEach(catagory => {
                            catagory.gameObject.SetActive(false);
                        });
                    });
                }
                rootCards.GetComponentInChildren<Toggle>().gameObject.SetActive(false);
                var text = ToggleCardsMenuHandler.scrollViews["CPC"].Find("Viewport/Content").gameObject.AddComponent<TextMeshProUGUI>();
                text.color = new Color(0.6f, 0.5f, 0.8f);
                text.text = "  " +
                "\r\n   Welcome to Chaos Poppycars Cars," +
                "\r\n   this is a modular mod created by <s>izzy</s> Poppy" +
                "\r\n   " +
                "\r\n   Cards added by different modules are separated into" +
                "\r\n   sub-categories seen to the right under the 'CPC' tab." +
                "\r\n   " +
                "\r\n   If you have any feedback, or would like to report a bug," +
                "\r\n   please reach out to @Poppycars in the RMC discord server." +
                "\r\n   ";
                text.fontSize = 20;
            });
        }

    }
}

*/