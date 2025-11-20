using Photon.Pun;
using SimulationChamber;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using System.Linq;
using ModdingUtils.MonoBehaviours;
using UnboundLib;

namespace CPCComplex.MonoBehaviours
{
    public class Destroyer : MonoBehaviour
    {
        public void Start()
        {
            Destroy(this.gameObject, 3);
        }
    }
}
