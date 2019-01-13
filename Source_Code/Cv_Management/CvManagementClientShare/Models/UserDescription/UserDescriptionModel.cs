namespace CvManagementClientShare.Models.UserDescription
{
    public class UserDescriptionModel
    {
        #region Properties

        public int Id { get; set; }

        public int UserId { get; set; }

        public string Description { get; set; }

        public double CreatedTime { get; set; }

        #endregion
    }
}