using Halic.Data.Abstract;
using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Halic.Data.Concrate.EfCore
{
    public class VideoRepository : GenericRepository<Video, HalicContext>, IVideoRepository
    {
        public List<Video> GetOrderOneAll()
        {
            using (var context = new HalicContext())
            {
                return context.Video.OrderByDescending(i => i.VideoId).Take(1).ToList();
            }
        }

        public List<Video> GetOrderThreeAll()
        {
            using(var context=new HalicContext())       
            {
                return context.Video.OrderByDescending(i => i.VideoId).Take(2).ToList();// 3'ten 2 ye indirildi
            }
        }
    }
}
