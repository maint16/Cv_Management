using ApiMultiPartFormData.Models;

namespace CvManagementClientShare.ViewModels.SkillCategory
{
    public class EditSkillCategoryViewModel
    {
        #region Properties
        /// <summary>
        /// Id of skill category
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Category photo.
        /// </summary>
        public HttpFile Photo { get; set; }
        
        /// <summary>
        /// Category name.
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}