using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using CvManagementBusiness.Interfaces.Domains;
using CvManagementBusiness.Interfaces.Services;
using CvManagementClientShare.Enums.SortProperties;
using CvManagementClientShare.Models.Skill;
using CvManagementClientShare.Models.SkillCategory;
using CvManagementClientShare.ViewModels;
using CvManagementClientShare.ViewModels.SkillCategory;
using CvManagementModel.Models;
using CvManagementModel.Models.Context;

namespace CvManagementBusiness.Domains
{
    public class SkillCategoryDomain : ISkilCategoryDomain
    {
        #region Properties

        private readonly CvManagementDbContext _dbContext;
        private readonly IDbService _dbService;

        #endregion

        #region Constructor

        public SkillCategoryDomain(DbContext dbContext, IDbService dbService)
        {
            _dbContext = (CvManagementDbContext)dbContext;
            _dbService = dbService;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<SearchResultViewModel<IList<SkillCategoryModel>>> SearchSkillCategoriesAsync(
            SearchSkillCategoryViewModel condition,
            CancellationToken cancellationToken = default(CancellationToken))
        {

            #region Information search

            // Get list of skill categories.
            var skillCategories = _dbContext.SkillCategories.AsQueryable();

            // Filter skill categories by indexes.
            if (condition.Ids != null && condition.Ids.Count > 0)
            {
                var ids = condition.Ids.Where(x => x > 0).ToList();
                if (ids.Count > 0)
                    skillCategories = skillCategories.Where(x => ids.Contains(x.Id));
            }

            // Filter skill categories by user indexes.
            if (condition.UserIds != null && condition.UserIds.Count > 0)
            {
                var userIds = condition.UserIds.Where(x => x > 0).ToList();
                if (userIds.Count > 0)
                    skillCategories = skillCategories.Where(x => userIds.Contains(x.UserId));
            }

            // Filter skill categories by user indexes.
            if (condition.Names != null && condition.Names.Count > 0)
            {
                var names = condition.Names.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                if (names.Count > 0)
                    skillCategories = skillCategories.Where(x => names.Any(name => x.Name.Contains(name)));
            }

            #endregion

            // Import skill list.
            var skills = Enumerable.Empty<Skill>().AsQueryable();
            var skillCategorySkillRelationships = Enumerable.Empty<SkillCategorySkillRelationship>().AsQueryable();

            if (condition.IncludeSkills)
            {
                skillCategorySkillRelationships = _dbContext.SkillCategorySkillRelationships.AsQueryable();
                skills = _dbContext.Skills.AsQueryable();
            }

            // Get offline skill categories.
            var loadSkillCategoryResult = new SearchResultViewModel<IList<SkillCategoryModel>>();
            loadSkillCategoryResult.Total = await skillCategories.CountAsync(cancellationToken);

            // Load skill categories.
            var loadedSkillCategories = from skillCategory in skillCategories
                                        select new SkillCategoryModel
                                        {
                                            Id = skillCategory.Id,
                                            UserId = skillCategory.UserId,
                                            Photo = skillCategory.Photo,
                                            Name = skillCategory.Name,
                                            CreatedTime = skillCategory.CreatedTime,
                                            Skills = from skill in skills
                                                     from skillCategorySkillRelationship in skillCategorySkillRelationships
                                                     where skillCategorySkillRelationship.SkillCategoryId == skillCategory.Id &&
                                                           skillCategorySkillRelationship.SkillId == skill.Id
                                                     select new SkillCategorySkillRelationshipModel
                                                     {
                                                         UserId = skillCategory.UserId,
                                                         SkillCategoryId = skillCategorySkillRelationship.SkillCategoryId,
                                                         SkillId = skillCategorySkillRelationship.SkillId,
                                                         Point = skillCategorySkillRelationship.Point,
                                                         SkillName = skill.Name
                                                     }
                                        };


            // Do sorting.
            loadedSkillCategories = _dbService.Sort(loadedSkillCategories, SortDirection.Ascending,
                SkillCategorySortProperty.Id);

            // Do paging.
            loadedSkillCategories = _dbService.Paginate(loadedSkillCategories, condition.Pagination);
            loadSkillCategoryResult.Records = await loadedSkillCategories.ToListAsync(cancellationToken);

            return loadSkillCategoryResult;
        }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<SkillCategory> AddSkillCategoryAsync(AddSkillCategoryViewModel model,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var skillCategory = new SkillCategory();

            skillCategory.Name = model.Name;
            skillCategory.UserId = model.UserId;
            if (model.Photo != null)
                skillCategory.Photo = Convert.ToBase64String(model.Photo.Buffer);
            skillCategory.CreatedTime = DateTime.Now.ToOADate();

            skillCategory = _dbContext.SkillCategories.Add(skillCategory);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return skillCategory;
        }

        public async Task<SkillCategory> EditCategoryAsync(EditSkillCategoryViewModel model,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            //Get SkillCategory
            var skillCategory = _dbContext.SkillCategories.Find(model);
            if (skillCategory == null)
                throw new Exception();

            //Update information
            if (!string.IsNullOrWhiteSpace(model.Name))
                skillCategory.Name = model.Name;

            // Photo is defined.
            //if (photo != null)
            //{
            //    var relativeSkillCategoryImagePath = await _fileService.AddFileToDirectory(model.Photo.Buffer, _appPath.ProfileImage, null, CancellationToken.None);
            //    skillCategory.Photo = Url.Content(relativeSkillCategoryImagePath);
            //}

            //Save change to db
            await _dbContext.SaveChangesAsync(cancellationToken);
            return skillCategory;
        }

        public async Task<int> DeleteAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var skillCategory = _dbContext.SkillCategories.Find(id);
            if (skillCategory == null)
                throw new Exception();

            _dbContext.SkillCategories.Remove(skillCategory);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }

        #endregion
    }
}