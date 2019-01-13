namespace CvManagementClientShare.Models.Skill
{
    public class SkillCategorySkillRelationshipModel
    {
        public int UserId { get; set; }

        public int SkillId { get; set; }

        public int SkillCategoryId { get; set; }

        public string SkillName { get; set; }

        public int Point { get; set; }
    }
}