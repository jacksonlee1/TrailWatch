using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.CommentModels;
using Models.PostModels;
using Models.TrailModels;

namespace TrailWatchMVC.Models
{
    public class TrailVM
    {
        public TrailDetail? _trail {get;set;}
        public PostCreate? _post {get;set;}

        public CommentCreate? _comment{get;set;}
    }
}