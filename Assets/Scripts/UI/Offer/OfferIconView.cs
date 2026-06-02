using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Offers
{
    public class OfferIconView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _count;
        [SerializeField] private Image _image;
        
        public void Setup(Sprite image, int count)
        {
            _image.sprite = image;
            _count.text = count.ToString();
        } 
    }
}