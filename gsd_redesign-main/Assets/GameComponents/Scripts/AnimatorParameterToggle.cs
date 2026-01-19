using UnityEngine;

public class AnimatorParameterToggle : MonoBehaviour
{
    Animator animator;
    [SerializeField] string triggerName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartAnimation()
    {
        animator.SetTrigger(triggerName);
    }

    public void SetBool(bool value)
    {
        animator.SetBool(triggerName, value);
    }
}
