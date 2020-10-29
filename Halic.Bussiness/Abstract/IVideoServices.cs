using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Bussiness.Abstract
{
    public interface IVideoServices
    {
        Video GetById(int id);
        List<Video> GetAll();
        void Create(Video Entity);
        void Update(Video Entity);
        void Delete(Video Entity);
        List<Video> GetOrderThreeAll();
        List<Video> GetOrderOneAll();

    }
}
