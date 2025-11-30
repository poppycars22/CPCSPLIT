using System.Linq;
using Nullmanager;
using TMPro;
using UnityEngine;
using UnboundLib;
using HarmonyLib;
using UnityEngine.UI;


namespace CPCCardInfostuffs
{
    public class CPCSICardInfo : MonoBehaviour
    {
        [Header("CPC Settings")]
        public CardInfo cardInfo;
        public GameObject cardBase;
        public string energy = "1";
        public bool sun = false;
        public bool moon = false;
        public bool fire = false;
        public bool air = false;
        public bool water = false;
        public bool earth = false;
        public bool plant = false;
        public bool animal = false;



        public void Start()
        {
            cardInfo = GetComponent<CardInfo>();
            cardBase = cardInfo.cardBase;
            GameObject energyGO = cardBase.GetComponentInChildren<Energy>().gameObject;

            //cardBase.GetComponent<CardInfoDisplayer>().effectText.transform.position -= Vector3.up * 0.3f;
            energyGO.GetComponent<TextMeshProUGUI>().text = energy;
            energyGO.GetComponent<TextMeshProUGUI>().color = cardBase.GetComponentInChildren<CardRarityColor>().GetComponent<Image>().color;

            GameObject sunG = cardBase.GetComponentInChildren<Sun>().gameObject;
            sunG.SetActive(sun);

            GameObject moonG = cardBase.GetComponentInChildren<Moon>().gameObject;
            moonG.SetActive(moon);

            GameObject fireG = cardBase.GetComponentInChildren<Fire>().gameObject;
            fireG.SetActive(fire);

            GameObject airG = cardBase.GetComponentInChildren<Air>().gameObject;
            airG.SetActive(air);

            GameObject waterG = cardBase.GetComponentInChildren<Water>().gameObject;
            waterG.SetActive(water);

            GameObject earthG = cardBase.GetComponentInChildren<Earth>().gameObject;
            earthG.SetActive(earth);

            GameObject plantG = cardBase.GetComponentInChildren<Plant>().gameObject;
            plantG.SetActive(plant);

            GameObject animalG = cardBase.GetComponentInChildren<Animal>().gameObject;
            animalG.SetActive(animal);

        }
    }
    
}
