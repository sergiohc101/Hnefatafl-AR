using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeLang : MonoBehaviour {

	public Sprite enSprite;
	public Sprite esSprite;
	private int lang;

	public Slider langSlider;

	private Image gObject;
	// Use this for initialization
	void Start () {
		gObject = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		lang = (int)langSlider.value;
		if (lang == 1) //lang = 1 for english, 0 for spanish
		{
			// If you want to change the sprite for only a short time,
			// and use a default whenever your condition is false
			//button.image.overrideSprite = newsprite;
			
			// But if you really want the source image,
			// use the following line instead
			//button.image.sprite = newsprite;
			gObject.sprite = enSprite;
		}
		else if(lang == 0)
		{
			// Setting the overrideSprite back to null will cause
			// the image to display the original value of image.sprite again
			//button.image.overrideSprite = null;
			gObject.sprite = esSprite;
		}
	}

	/*public void ChangeLanguage(int lang) //lang = 1 for english, 0 for spanish
	{

	}*/
}
