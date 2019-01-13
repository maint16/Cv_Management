using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using CvManagementBusiness.Interfaces.Domains;
using CvManagementBusiness.Interfaces.Services;
using CvManagementClientShare.Enums.SortProperties;
using CvManagementClientShare.Models.Project;
using CvManagementClientShare.Models.Responsibility;
using CvManagementClientShare.Models.Skill;
using CvManagementClientShare.ViewModels;
using CvManagementClientShare.ViewModels.Project;
using CvManagementModel.Models;
using CvManagementModel.Models.Context;

namespace CvManagementBusiness.Domains
{
    public class ProjectDomain : IProjectDomain
    {
        #region Properties

        private readonly CvManagementDbContext _dbContext;
        private readonly IDbService _dbService;

        #endregion

        #region Contructor

        public ProjectDomain(DbContext dbContext, IDbService dbService)
        {
            _dbContext = (CvManagementDbContext)dbContext;
            _dbService = dbService;
        }

        #endregion

        #region Methods




        public async Task<SearchResultViewModel<IList<ProjectModel>>> SearchProjectAsync(SearchProjectViewModel condition, CancellationToken cancellationToken = default(CancellationToken))
        {
            var projects = _dbContext.Projects.AsQueryable();
            if (condition.Ids != null)
            {
                var ids = condition.Ids.Where(x => x > 0).ToList();
                if (ids.Count > 0)
                    projects = projects.Where(x => ids.Contains(x.Id));
            }

            if (condition.Names != null)
            {
                var names = condition.Names.Where(c => !string.IsNullOrEmpty(c)).ToList();
                if (names.Count > 0)
                    projects = projects.Where(c => condition.Names.Contains(c.Name));
            }

            if (condition.UserIds != null)
            {
                var userIds = condition.UserIds.Where(c => c > 0).ToList();
                if (userIds.Count > 0)
                    projects = projects.Where(c => condition.UserIds.Contains(c.UserId));
            }

            if (condition.StartedTime != null)
                projects = projects.Where(c => c.StartedTime >= condition.StartedTime.From
                                               && c.StartedTime <= condition.StartedTime.To);

            if (condition.FinishedTime != null)
                projects = projects.Where(c => c.FinishedTime >= condition.FinishedTime.From
                                               && c.FinishedTime <= condition.FinishedTime.To);

            #region Search project skills & responsibilities.

            var skills = Enumerable.Empty<Skill>().AsQueryable();
            var projectSkills = Enumerable.Empty<ProjectSkill>().AsQueryable();

            if (condition.IncludeSkills)
            {
                skills = _dbContext.Skills.AsQueryable();
                projectSkills = _dbContext.ProjectSkills.AsQueryable();
            }


            var responsibilities = Enumerable.Empty<Responsibility>().AsQueryable();
            var projectResponsibilities = Enumerable.Empty<ProjectResponsibility>().AsQueryable();
            if (condition.IncludeResponsibilities)
            {
                responsibilities = _dbContext.Responsibilities.AsQueryable();
                projectResponsibilities = _dbContext.ProjectResponsibilities.AsQueryable();
            }

            var loadedProjects = from project in projects
                                 select new ProjectModel
                                 {
                                     Id = project.Id,
                                     UserId = project.UserId,
                                     Name = project.Name,
                                     Description = project.Description,
                                     StartedTime = project.StartedTime,
                                     FinishedTime = project.FinishedTime,
                                     Skills = from projectSkill in projectSkills
                                              from skill in skills
                                              where projectSkill.ProjectId == project.Id && projectSkill.SkillId == skill.Id
                                              select new SkillModel
                                              {
                                                  Id = skill.Id,
                                                  Name = skill.Name,
                                                  CreatedTime = skill.CreatedTime,
                                                  LastModifiedTime = skill.LastModifiedTime
                                              },
                                     Responsibilities = from projectResponsibility in projectResponsibilities
                                                        from responsibility in responsibilities
                                                        where projectResponsibility.ProjectId == project.Id &&
                                                              projectResponsibility.ResponsibilityId == responsibility.Id
                                                        select new ResponsibilityModel
                                                        {
                                                            Id = responsibility.Id,
                                                            Name = responsibility.Name,
                                                            CreatedTime = responsibility.CreatedTime,
                                                            LastModifiedTime = responsibility.LastModifiedTime
                                                        }
                                 };

            #endregion

            var result = new SearchResultViewModel<IList<ProjectModel>>();
            result.Total = await projects.CountAsync(cancellationToken);

            //Do sort
            loadedProjects = _dbService.Sort(loadedProjects, SortDirection.Ascending, ProjectSortProperty.Id);

            //Do Pagination
            loadedProjects = _dbService.Paginate(loadedProjects, condition.Pagination);

            result.Records = await loadedProjects.ToListAsync(cancellationToken);
            return result;
        }

        public async Task<Project> AddProjectAsync(AddProjectViewModel model,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            //Check exists project in database
            var isExists = await _dbContext.Projects.AnyAsync(c => c.Name == model.Name, cancellationToken);
            if (isExists)
                throw new Exception();

            var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                //Inial Project object
                var project = new Project();

                project.UserId = model.UserId;
                project.Name = model.Name;
                project.Description = model.Description;
                project.FinishedTime = model.FinishedTime;
                project.StartedTime = model.StatedTime;

                //Add project to database
                project = _dbContext.Projects.Add(project);
                if (model.SkillIds != null)
                {
                    #region add project skill

                    //check exists skill
                    var countSkills = await _dbContext.Skills.Where(c => model.SkillIds.Contains(c.Id)).CountAsync();
                    var isExistSkills = countSkills == model.SkillIds.Count;

                    if (!isExistSkills)
                        throw new Exception();

                    //Insert to projectSkill table
                    foreach (var skillId in model.SkillIds)
                    {
                        var projectSkill = new ProjectSkill();

                        projectSkill.ProjectId = project.Id;
                        projectSkill.SkillId = skillId;

                        //Add to db context
                        _dbContext.ProjectSkills.Add(projectSkill);
                    }

                    #endregion
                }

                if (model.ResponsibilityIds != null)
                {
                    #region Project responsibilitis

                    // check exists responsibilities
                    var countRespon = await _dbContext.Responsibilities
                        .Where(c => model.ResponsibilityIds.Contains(c.Id)).CountAsync();
                    var isExistsRespon = countRespon == model.ResponsibilityIds.Count;

                    if (!isExistsRespon)
                        throw new Exception();

                    //Insert project responsibility to db context
                    foreach (var responsibilityId in model.ResponsibilityIds)
                    {
                        var projectResponsibility = new ProjectResponsibility();

                        projectResponsibility.ProjectId = project.Id;
                        projectResponsibility.ResponsibilityId = responsibilityId;
                        projectResponsibility.CreatedTime = DateTime.UtcNow.ToOADate();

                        //Add to db context
                        _dbContext.ProjectResponsibilities.Add(projectResponsibility);
                    }

                    #endregion
                }


                //Save changes to database
                await _dbContext.SaveChangesAsync(cancellationToken);

                //Commit transaction
                transaction.Commit();
                return project;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new Exception(e.Message);
            }

        }

        public async Task<Project> EditProjectAsync(EditProjectViewModel model, CancellationToken cancellationToken = default(CancellationToken))
        {
            //Find  Project
            var project = await _dbContext.Projects.FindAsync(model.Id, cancellationToken);
            if (project == null)
                throw new Exception();

            //Update information
            project.UserId = model.UserId;
            project.Name = model.Name;
            project.Description = model.Description;
            project.FinishedTime = model.FinishedTime;
            project.StartedTime = model.StatedTime;

            #region  Update skills

            //Remove skill
            if (model.SkillIds != null)
            {
                var skills = _dbContext.Skills.Where(c => model.SkillIds.Contains(c.Id));
                var isExists = skills.Count() == model.SkillIds.Count;
                if (!isExists)
                    throw new Exception();

                foreach (var projectSkill in project.ProjectSkills.ToList())
                    project.ProjectSkills.Remove(projectSkill);
            }

            #endregion

            //Save changes to database
            await _dbContext.SaveChangesAsync(cancellationToken);

            return project;
        }

        public async Task<int> DeleteProjectAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            //Find project in database
            var project = _dbContext.Projects.Find(id);

            if (project == null)
                throw new Exception();

            //Delete project from database
            _dbContext.Projects.Remove(project);

            //Save changes to database
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
        #endregion
    }
}
