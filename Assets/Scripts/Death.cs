using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public class Death : MonoBehaviour
    {
        public event Action OnDeath;
        
        [SerializeField] private IntValue hp;
        
        private void OnEnable()
        {
            hp.OnValueChanged += CheckDeath;
        }

        private void OnDisable()
        {
            hp.OnValueChanged -= CheckDeath;
        }

        private void CheckDeath(int hp)
        {
            if (hp <= 0)
            {
                OnDeath?.Invoke();
            }
        }
    }
}
