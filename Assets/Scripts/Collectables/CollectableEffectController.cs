using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableEffectController : MonoBehaviour {

    public static CollectableEffectController instance;
    public Image zemImage;
    public Text zemText;

    Animator _animator;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start () {
        _animator = GetComponent<Animator>();
	}


    public void PlayCollectableCollectEffect(Sprite sprite, VariableUtilities.collectableType type)
    {
        _animator.SetTrigger("is_play_effect");
        int amount = DataHandler.instance.GetSpecificCollectableStoredNo(type);
        zemText.text = amount.ToString();
        zemImage.sprite = sprite;
    }
}
