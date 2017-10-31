using Matricis.Helpers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matricis.Models {
    public class BaseModel : ObservableObject {

        /// <summary>
        /// Azure created at time stamp
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public String Id { get; set; }

        /// <summary>
        /// Azure created at time stamp
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// Azure created at time stamp
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Azure UpdateAt timestamp for online/offline sync
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        public BaseModel() {
            Id = Guid.NewGuid().ToString();
        }
    }
}