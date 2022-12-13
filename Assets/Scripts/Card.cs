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

        public void Init(CardData data, Texture2D image)
        {
            art.SetArt(image);
            title.SetText(data.Title);
            description.SetText(data.Description);
            attack.Assign(data.Attack);
            hp.Assign(data.HP);
            mana.Assign(data.Mana);
        }

        public void SubscribeOnDeath(Action func)
        {
            death.OnDeath += func;
        }
    }
}
