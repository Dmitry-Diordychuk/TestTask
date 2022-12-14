using TMPro;
using UnityEngine;

namespace TestTask
{
    public class Text : MonoBehaviour
    {
        private TextMeshProUGUI _textMeshPro;

        private void Awake()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        public void SetText(string text)
        {
            _textMeshPro.text = text;
        }

        public override string ToString()
        {
            return _textMeshPro.text;
        }
    }
}
