using Halic.Bussiness.Abstract;
using Halic.Data.Abstract;
using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Bussiness.Concrate
{
    public class VideoManager : IVideoServices
    {
        private IVideoRepository _videoRepository;
        public VideoManager(IVideoRepository videoRepository)   
        {
            _videoRepository = videoRepository;
        }
        public void Create(Video Entity)
        {
            _videoRepository.Create(Entity);
        }

        public void Delete(Video Entity)
        {
            _videoRepository.Delete(Entity);
        }

        public List<Video> GetAll()
        {
            return _videoRepository.GetAll();
        }

        public Video GetById(int id)
        {
           return _videoRepository.GetById(id);
        }

        public List<Video> GetOrderOneAll()
        {
            return _videoRepository.GetOrderOneAll();
        }

        public List<Video> GetOrderThreeAll()
        {
           return _videoRepository.GetOrderThreeAll();
        }

        public void Update(Video Entity)
        {
            _videoRepository.Update(Entity);
        }
    }
}
