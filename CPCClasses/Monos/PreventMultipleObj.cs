using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnityEngine;

namespace CPCClasses.MonoBehaviours
{
    public class PreventMultipleObj : MonoBehaviour
    {
        public void Awake()
        {
            ChaosPoppycarsCardsClasses.Instance.ExecuteAfterFrames(1, () => { 
            if (!transform.parent.gameObject.GetComponentInChildren(this.GetType()).Equals(this))
                DestroyImmediate(this.gameObject);
            });
        }
    }
}
