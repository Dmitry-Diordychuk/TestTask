using UnityEngine;
using UnityEngine.UI;

namespace TestTask
{
    public class Art : MonoBehaviour
    {
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void SetArt(Texture2D art)
        {
            _image.material.mainTexture = art;
        }
    }
}
