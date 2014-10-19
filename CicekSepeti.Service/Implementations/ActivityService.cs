using System;
using System.Collections.Generic;
using System.Linq;
using CicekSepeti.Entity;
using CicekSepeti.Model;
using CicekSepeti.Repository.Contracts;
using CicekSepeti.Service.Contracts;
using FastMapper;

namespace CicekSepeti.Service.Implementations
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepo;
        private readonly IUnitOfWork _uow;

        public ActivityService(IActivityRepository activityRepo, IUnitOfWork uow)
        {
            _activityRepo = activityRepo;
            _uow = uow;
        }


        public List<ActivityDTO> GetActivitiesByUserId(Guid id)
        {
            var activities = _activityRepo.GetMany(a => a.UserId.Equals(id)).OrderByDescending(a=> a.LoginDateTime).ToList();

            return TypeAdapter.Adapt<List<Activity>, List<ActivityDTO>>(activities);
        }
    }
}