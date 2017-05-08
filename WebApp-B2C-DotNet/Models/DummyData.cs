using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp_OpenIDConnect_DotNet_B2C.Models
{
    public class DummyData
    {
        public String order;
        public String desc;
        public String date;
        public String color;

        public DummyData(String order, String desc, String date, String color)
        {
            this.order = order;
            this.desc = desc;
            this.date = date;
            this.color = color;
        }
    }

}