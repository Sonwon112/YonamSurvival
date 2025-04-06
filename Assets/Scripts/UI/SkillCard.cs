using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCard : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image cardProfile;
    [SerializeField] private TMP_Text cardDescript;

    private Skill mSkill;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSkil(Skill mSkill)
    {
        this.mSkill = mSkill;
        cardProfile.sprite = this.mSkill.getSkillProfile();
        cardDescript.text = this.mSkill.getSkillDescript();
    }

    public void Pick()
    {
        GameManager.Instance.SelectedSkill(mSkill);
    }

}
