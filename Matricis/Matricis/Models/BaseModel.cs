﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matricis.Models {
    public class BaseModel {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public String Title { get; set; }
    }
}