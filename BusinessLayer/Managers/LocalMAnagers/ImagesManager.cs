using BusinessLayer.QueryResult;
using Entities;
using Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ImagesManager:ManagerBase<VisitImages>
    {
        public BusinessLayerResult<VisitImages> SaveImages(List<VisitImages> data)
        {
            BusinessLayerResult<VisitImages> imageData = new BusinessLayerResult<VisitImages>();
            foreach (var item in data)
            {
                if (base.Insert(item) == 0)
                {
                    imageData.AddError(ErrorMessageCode.DataInsertError, "Xəta başverdi.Qeyd Tamamlanmadı.");
                }
            }            
            return imageData;
        }
    }
}
