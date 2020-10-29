using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Data.Abstract
{
    public interface IVideoRepository : IRepository<Video>
    {
        List<Video> GetOrderThreeAll();
        List<Video> GetOrderOneAll();               
    }
}
