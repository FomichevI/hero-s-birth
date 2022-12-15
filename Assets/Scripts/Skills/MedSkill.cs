public class MedSkill : Skill
{
    private void FixedUpdate()
    {
        if (_durationSkill > 0)
        {
            _durationSkill -= 0.02f;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
