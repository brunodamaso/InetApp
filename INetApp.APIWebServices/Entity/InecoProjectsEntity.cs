using INetApp.APIWebServices.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace INetApp.APIWebServices.Entity
{
    public class InecoProjectsEntity : Response
    {
        public List<InecoProjectEntity> InecoProjectsEntities;
    }
}