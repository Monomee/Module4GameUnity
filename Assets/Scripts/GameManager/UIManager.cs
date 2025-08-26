using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private void OnEnable()
    {
        Instance = this;
    }
    private void OnDisable()
    {
        Instance = null;
    }
    public Slider HPSlider;
    public Image Skill1;
    public Image Skill2;
    // Start is called before the first frame update
    void Start()
    {
        Skill1.type = Image.Type.Filled;
        Skill2.type = Image.Type.Filled;
        Skill1.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnUsed(int index, float cooldown)
    {
        Image skillImage = (index == 0 ? Skill1 : Skill2);
        StartCoroutine(CoolDown(skillImage, cooldown));
    }
    IEnumerator CoolDown(Image skillImage, float cooldown)
    {
        float time = 0;
        while (time < cooldown)
        {
            skillImage.fillAmount = time / cooldown; 
            time += Time.deltaTime;
            yield return null;
        }
        skillImage.fillAmount = 1;
    }
}
