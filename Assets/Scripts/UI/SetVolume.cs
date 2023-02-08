using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    public string nameVolume;
    private Slider slider;
    private GameManager GM;
    private float sliderT;

    private void Start()
    {
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    private void OnEnable()
    {
        slider = GetComponent<Slider>();
        GM = GameObject.FindObjectOfType<GameManager>();

        if (nameVolume == "_musicVolume")
        {
            mixer.SetFloat(nameVolume, Mathf.Log10(GM.musicVolume) * 20);
            slider.value = GM.musicVolume;
        } 
        else if (nameVolume == "_soundVolume")
        {
            mixer.SetFloat(nameVolume, Mathf.Log10(GM.soundVolume) * 20);
            slider.value = GM.soundVolume;
        }
    }
    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat(nameVolume, Mathf.Log10(sliderValue) * 20);
        if (nameVolume == "_musicVolume")
        {
            GM.musicVolume = sliderValue;
        }
        else if (nameVolume == "_soundVolume") 
        {
            GM.soundVolume = sliderValue;
        }
    }
    public void ValueChangeCheck()
    {
        Vector3 listener = GameObject.Find("Listener").gameObject.transform.position;
        if (nameVolume == "_soundVolume")
        {
            if (Mathf.Abs(sliderT - slider.value) > 0.15f)
            {
                SoundManager.Instance.PlayAnyway("explosion", listener);
                sliderT = slider.value;
            } 
                      
        }
        
    }
}
