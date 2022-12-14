using System;
using UnityEngine;

namespace TestTask
{
    public class Card : MonoBehaviour
    {
        public event Action<Card> OnDeath; 

        [SerializeField] private Art art;
        [SerializeField] private Text title;
        [SerializeField] private Text description;

        [SerializeField] private Death death;
        public IntValue attack;
        public IntValue hp;
        public IntValue mana;

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

        private void OnEnable()
        {
            death.OnDeath += OnCardDeath;
        }

        private void OnDisable()
        {
            death.OnDeath -= OnCardDeath;
        }

        private void OnCardDeath()
        {
            OnDeath?.Invoke(this);
        }

        public override string ToString()
        {
            return title.ToString();
        }
    }
}
