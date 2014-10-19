
using System;
using System.Collections.Generic;
using System.Linq;
using CicekSepeti.Entity;
using CicekSepeti.Model;

namespace CicekSepeti.Service.Contracts
{
    public interface IActivityService
    {
        List<ActivityDTO> GetActivitiesByUserId(Guid id);
    }
}
