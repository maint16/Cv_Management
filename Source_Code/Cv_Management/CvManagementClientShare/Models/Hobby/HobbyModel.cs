namespace CvManagementClientShare.Models.Hobby
{
    public class HobbyModel
    {
        /// <summary>
        ///     Id of hobby
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Name of hobby
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     user that hobby belong to
        /// </summary>

        public int UserId { get; set; }

        /// <summary>
        ///     Description of hobby
        /// </summary>

        public string Description { get; set; }
    }
}