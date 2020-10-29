using Halic.Bussiness.Abstract;
using Halic.Data.Abstract;
using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Bussiness.Concrate
{
    public class SliderManager : ISliderServices
    {
        private ISliderRepository _sliderRepository;
        public SliderManager(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }
        public void Create(Slider Entity)
        {
            _sliderRepository.Create(Entity);
        }

        public void Delete(Slider Entity)
        {
            _sliderRepository.Delete(Entity);
        }

        public List<Slider> GetAll()
        {
            return _sliderRepository.GetAll();
        }

        public Slider GetById(int id)
        {
            return _sliderRepository.GetById(id);
        }

        public List<Slider> GetOrderAll()
        {
            return _sliderRepository.GetOrderAll();
        }

        public void Update(Slider Entity)
        {
            _sliderRepository.Update(Entity);
        }
    }
}
