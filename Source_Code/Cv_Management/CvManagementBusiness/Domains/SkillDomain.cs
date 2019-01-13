using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using CvManagementBusiness.Interfaces.Domains;
using CvManagementBusiness.Interfaces.Services;
using CvManagementClientShare.Enums.SortProperties;
using CvManagementClientShare.ViewModels;
using CvManagementClientShare.ViewModels.Skill;
using CvManagementModel.Models;
using CvManagementModel.Models.Context;

namespace CvManagementBusiness.Domains
{
    public class SkillDomain : ISkillDomain
    {
        #region Properties

        private readonly CvManagementDbContext _dbContext;
        private readonly IDbService _dbService;

        #endregion

        #region Contructor

        public SkillDomain(DbContext dbContext, IDbService dbService)
        {
            _dbContext = (CvManagementDbContext)dbContext;
            _dbService = dbService;
        }

        #endregion

        #region Methods

        public async Task<SearchResultViewModel<IList<Skill>>> SearchSkillsAsync(SearchSkillViewModel condition,
            CancellationToken cancellationToken = default(CancellationToken))
        {

            var skills = _dbContext.Skills.AsQueryable();

            if (condition.Ids != null && condition.Ids.Count > 0)
            {
                var ids = condition.Ids.Where(c => c > 0).ToList();
                if (ids.Count > 0)
                    skills = skills.Where(c => ids.Contains(c.Id));
            }

            if (condition.Names != null && condition.Names.Count > 0)
            {
                var names = condition.Names.Where(c => !string.IsNullOrEmpty(c)).ToList();
                if (names.Count > 0)
                    skills = skills.Where(c => names.Contains(c.Name));
            }

            if (condition.StartedTime != null)
                skills = skills.Where(c => c.CreatedTime >= condition.StartedTime.From
                                           && c.CreatedTime <= condition.StartedTime.To);

            var result = new SearchResultViewModel<IList<Skill>>();
            result.Total = await skills.CountAsync(cancellationToken);

            //do sorting
            skills = _dbService.Sort(skills, SortDirection.Ascending, UserDescriptionSortProperty.Id);

            // Do pagination.
            skills = _dbService.Paginate(skills, condition.Pagination);

            result.Records = await skills.ToListAsync(cancellationToken);

            return result;
        }

        public async Task<Skill> AddSkillAsync(AddSkillViewModel model, CancellationToken cancellationToken = default(CancellationToken))
        {
            //Check exists skill in database
            var isExists = await _dbContext.Skills.AnyAsync(c => c.Name.Equals(model.Name), cancellationToken);
            if (isExists)
                throw new Exception();

            //Inital skill object
            var skill = new Skill();
            skill.Name = model.Name;
            skill.CreatedTime = DateTime.Now.ToOADate();

            //add skill to database
            skill = _dbContext.Skills.Add(skill);

            //save changes to database
            await _dbContext.SaveChangesAsync(cancellationToken);

            return skill;

        }

        public async Task<Skill> EditSkillAsync(EditSkillViewModel model, CancellationToken cancellationToken = default(CancellationToken))
        {
            //Find skill in database
            var skill = await _dbContext.Skills.FindAsync(model.Id, cancellationToken);
            if (skill == null)
                throw new Exception();

            //Update information
            skill.Name = model.Name;
            skill.LastModifiedTime = DateTime.Now.ToOADate();

            //Save changes to database
            await _dbContext.SaveChangesAsync(cancellationToken);

            return skill;
        }

        public async Task<int> DeleteSkillAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            //Find skill in database
            var skill = await _dbContext.Skills.FindAsync(id);
            if (skill == null)
                throw new Exception();

            //Delete skill from database
            _dbContext.Skills.Remove(skill);

            //Save changes in database
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }

        #endregion

    }
}