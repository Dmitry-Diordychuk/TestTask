using UnityEngine;

namespace TestTask
{
	[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
	public class CardData : ScriptableObject
	{
		public Sprite Art; // + UI overlay
		public string Title;
		public string Description;
		public int Attack; // icon + text value
		public int HP; // icon + text value
		public int Mana; // icon + text value
	}
}