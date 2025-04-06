using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("SkillCard")]
    [SerializeField] private SkillCard[] skillCards; 

    private Animator uiAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        uiAnimator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSkills(int index, Skill skill)
    {
        skillCards[index].setSkil(skill);

        //ShowCard();
    }

    public void ShowCard()
    {
        uiAnimator.SetBool("showCard", true);
    }

    public void HideCard()
    {
        uiAnimator.SetBool("showCard", false);
    }

    public void HideDone()
    {
        GameManager.Instance.Resume();
    }

    
}
