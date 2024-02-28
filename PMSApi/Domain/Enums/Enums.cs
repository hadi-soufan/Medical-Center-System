using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public class Enums
    {
        public enum AppointmentStatus
        {
            Pending,
            Confirmed,
            Cancelled
        }

        public enum AppointmentStatusAr
        {
            قيد_الانتظار,
            تم_التأكيد,
            تم_الإلغاء
        }

        public enum AppointmentType
        {
            OnSite,
            Remote
        }

        public enum AppointmentTypeAr
        {
            في_الموقع, 
            عن_بُعد 
        }



    }
}
