using System;
using UnityEngine;

namespace TestTask
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Art art;
        [SerializeField] private Text title;
        [SerializeField] private Text description;
        [SerializeField] private IntValue attack;
        [SerializeField] private IntValue hp;
        [SerializeField] private IntValue mana;
        [SerializeField] private Death death;

        private CardData _cardData;
        
        public void Init(CardData data)
        {
            _cardData = data;
            
            title.SetText(_cardData.Title);
            description.SetText(_cardData.Description);
            attack.Assign(_cardData.Attack);
            hp.Assign(_cardData.HP);
            mana.Assign(_cardData.Mana);
        }

        public void UpdateArt(Texture2D image)
        {
            art.SetArt(image);
        }

        public void SubscribeOnDeath(Action func)
        {
            death.OnDeath += func;
        }
    }
}
