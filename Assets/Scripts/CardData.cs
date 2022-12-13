using UnityEngine;

namespace TestTask
{
	[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
	public class CardData : ScriptableObject
	{
		public string Title;
		public string Description;
		public int Attack;
		public int HP;
		public int Mana;
	}
}