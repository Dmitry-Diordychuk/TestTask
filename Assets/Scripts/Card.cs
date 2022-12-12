using UnityEngine;

namespace TestTask
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private CardData data;
        [SerializeField] private IntValue attack;
        [SerializeField] private IntValue hp;
        [SerializeField] private IntValue mana;
        [SerializeField] private Death death;

        private void OnEnable()
        {
            attack.Assign(data.Attack);
            hp.Assign(data.HP);
            mana.Assign(data.Mana);
        }
    }
}
