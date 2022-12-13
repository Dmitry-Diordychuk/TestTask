using System;
using UnityEngine;

namespace TestTask
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Art art;
        [SerializeField] private Text title;
        [SerializeField] private Text description;

        public IntValue attack;
        public IntValue hp;
        public IntValue mana;
        public Death death;

        private CardData _data;

        public CardData Data
        {
            set
            {
                _data = value;
                art.SetArt(value.Art);
                title.SetText(value.Title);
                description.SetText(value.Description);
                attack.Assign(value.Attack);
                hp.Assign(value.HP);
                mana.Assign(value.Mana);
            }
        }
    }
}
